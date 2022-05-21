﻿using LitJson;

namespace Server.Emulator;

public class PlaceholderServerData
{
    public static cometScene.Ret_Event_Info Ret_Event_Info = JsonMapper.ToObject<cometScene.Ret_Event_Info>(@"
         {
            ""levelGift"":{
               ""getList"":[
                  5,
                  2
               ]
            },
            ""getStamina"":{
               ""isGet"":0
            },
            ""newPlayer"":{
             ""loginDay"":7
            },
            ""weekCheckin"":{
             ""loginDay"":2,
               ""rewardList"":[
                  {
                 ""day"":1,
                     ""reward"":{
                     ""type"":1,
                        ""count"":10,
                        ""id"":0
                     }
             },
                  {
                 ""day"":7,
                     ""reward"":{
                     ""type"":3,
                        ""count"":1,
                        ""id"":90005
                     }
             },
                  {
                 ""day"":6,
                     ""reward"":{
                     ""type"":3,
                        ""count"":1,
                        ""id"":90002
                     }
             },
                  {
                 ""day"":5,
                     ""reward"":{
                     ""type"":3,
                        ""count"":1,
                        ""id"":90001
                     }
             },
                  {
                 ""day"":4,
                     ""reward"":{
                     ""type"":1,
                        ""count"":10,
                        ""id"":0
                     }
             },
                  {
                 ""day"":3,
                     ""reward"":{
                     ""type"":3,
                        ""count"":1,
                        ""id"":90001
                     }
             },
                  {
                 ""day"":2,
                     ""reward"":{
                     ""type"":3,
                        ""count"":1,
                        ""id"":90002
                     }
             }
               ]
            },
            ""bili"":{
             ""beginTime"":""1530374400"",
               ""endTime"":""1893427200"",
               ""id"":0,
               ""list"":[
                  {
                 ""index"":1,
                     ""type"":4,
                     ""condition"":1,
                     ""value"":0,
                     ""isGet"":1,
                     ""rewardList"":[
                        {
                             ""type"":4,
                             ""count"":1,
                             ""id"":12
                        }
                     ]
                  },
                  {
                 ""index"":4,
                     ""type"":4,
                     ""condition"":2,
                     ""value"":0,
                     ""isGet"":1,
                     ""rewardList"":[
                        {
                     ""type"":5,
                           ""count"":1,
                           ""id"":20020
                        }
                     ]
                  },
                  {
                 ""index"":3,
                     ""type"":4,
                     ""condition"":1,
                     ""value"":0,
                     ""isGet"":1,
                     ""rewardList"":[
                        {
                     ""type"":5,
                           ""count"":1,
                           ""id"":50010
                        }
                     ]
                  },
                  {
                 ""index"":2,
                     ""type"":4,
                     ""condition"":1,
                     ""value"":0,
                     ""isGet"":1,
                     ""rewardList"":[
                        {
                     ""type"":4,
                           ""count"":1,
                           ""id"":8
                        }
                     ]
                  }
               ]
            }
         }
     ");
}