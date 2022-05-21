﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using ProtoBuf;

namespace Server.Emulator.Handlers;

public class Login
{
    private Dictionary<uint, Action<uint, uint, byte[]>> Handlers = new();

    public Login()
    {
        Handlers.Add((uint)cometLogin.ParaCmd.ParaCmd_Req_GameVersion, (uint mainCmd, uint paraCmd, byte[] msgContent) =>
        {
            string gameVersion = Aquatrax.GlobalConfig.getInstance().getGameVersion();
            ServerLogger.LogInfo($"GameVersion: {gameVersion}");

            Index.GamePackage gamePackage = new()
            {
                MainCmd = (uint)cometLogin.MainCmd.MainCmd_Login,
                ParaCmd = (uint)cometLogin.ParaCmd.ParaCmd_Ret_GameVersion,
                Data = Index.ObjectToByteArray(new cometLogin.Ret_GameVersion()
                {
                    version = gameVersion,
                    announcementContent = "",
                    announcementTitle = "",
                    serverState = 2
                }),
            };

            Index.Instance.LoginPackageQueue.Enqueue(gamePackage);
        });
        
        Handlers.Add((uint)cometLogin.ParaCmd.ParaCmd_Req_ThirdLogin, (uint mainCmd, uint paraCmd, byte[] msgContent) =>
        {
            ServerLogger.LogInfo($"ThirdLogin");
            cometLogin.Req_ThirdLogin data = Serializer.Deserialize<cometLogin.Req_ThirdLogin>(new MemoryStream(msgContent));

            if (data == null)
                return;

            string token;
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] hash = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(data.openId + "6031"));
                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < hash.Length; i++)
                {
                    sBuilder.Append(hash[i].ToString("x2"));
                }

                token = sBuilder.ToString();
            }

            DataBase.Datatypes.AccountData accountData = Server.Database.GetAccount(data.openId);
            uint accId;
            if (accountData == null)
            {
                accId = Server.Database.GetAccountId(data.openId);
                accountData = new DataBase.Datatypes.AccountData()
                {
                    accId = accId,
                    steamId = data.openId,
                    token = token,
                };
                Server.Database.SetAccount(data.openId, accountData);
                Server.Database.Save();
            }
            else
            {
                accId = accountData.accId;
            }

            ServerLogger.LogInfo($"Token: {token}, AccId: {accId}");

            Index.GamePackage gamePackage = new Index.GamePackage()
            {
                MainCmd = (uint)cometLogin.MainCmd.MainCmd_Login,
                ParaCmd = (uint)cometLogin.ParaCmd.ParaCmd_Ret_ThirdLogin,
                Data = Index.ObjectToByteArray(new cometLogin.Ret_ThirdLogin()
                {
                    data = new cometLogin.GatewayServerData()
                    {
                        gateIP = "127.0.0.1",
                        gatePort = 20021,
                        token = token,
                        accId = accId
                    },
                }),
            };

            Index.Instance.LoginPackageQueue.Enqueue(gamePackage);
        });
    }
    
    public bool Dispatch(uint mainCmd, uint paraCmd, byte[] msgContent)
    {
        if (!Handlers.ContainsKey(paraCmd)) return false;

        Handlers[paraCmd](mainCmd, paraCmd, msgContent);
        return true;
    }
}