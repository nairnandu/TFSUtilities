
////Copyright 2017 Nandu Muralidharan

////Licensed under the Apache License, Version 2.0 (the "License");
////you may not use this file except in compliance with the License.
////You may obtain a copy of the License at

////    http://www.apache.org/licenses/LICENSE-2.0

////Unless required by applicable law or agreed to in writing, software
////distributed under the License is distributed on an "AS IS" BASIS,
////WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
////See the License for the specific language governing permissions and
////limitations under the License.

using System;
using System.Collections.Generic;
using System.IO;
using TFSPermissionsHelpers;

namespace TFSSecurityConsole
{
    class Program
    {
        private static char[] User2GroupsAndProjectsSplitter = new char[] { ',' };
        private static char[] GroupsAndProjectsSplitter = new char[] { '|' };
        static void Main(string[] args)
        {
            string[] strAllText = File.ReadAllLines("User2GroupsMap.csv");
            List <User2GroupsMap> user2GroupsMap = new List<User2GroupsMap>();

            foreach (string strLine in strAllText)
            {
                if (!strLine.StartsWith("#") && !string.IsNullOrEmpty(strLine.Trim()))
                {
                    User2GroupsMap userMap = null;
                    string[] arr = strLine.Split(User2GroupsAndProjectsSplitter, StringSplitOptions.RemoveEmptyEntries);
                    if (arr.Length < 3)
                        Console.WriteLine("Error");
                    else
                    {
                        userMap = new User2GroupsMap(arr[0], new List<string>(arr[1].Split(GroupsAndProjectsSplitter)),
                            new List<string>(arr[2].Split(GroupsAndProjectsSplitter)));
                        user2GroupsMap.Add(userMap);
                    }
                }
            }

            //List <User2GroupsMap> user2grpMap = FileHelper.ReadUser2GroupsMapFromFile("User2GroupsMap.json");

            //PermissionManager pm = new PermissionManager(new System.Uri("http://localhost:8080/tfs/DefaultCollection"));
            //pm.AddUsersToGroups(user2grpMap);

            System.Console.ReadLine();
        }
    }
}
