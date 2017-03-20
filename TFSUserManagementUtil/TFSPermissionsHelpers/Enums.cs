
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

namespace TFSPermissionsHelpers
{
    public enum InputFileType
    {
        CSV,
        JSON
    }

    public enum ErrorCodes
    {
        UserNotFoundOrInvalid,
        InvalidCollectionURL,
        TeamProjectNotFound,
        InsufficientPermissions,
        UserAlreadyInGroup
    }

    public enum ReturnCode
    {
        Failure = 0,
        PartialSuccess = 1,
        Success = 2
    }

    public static class Constants
    {
        public const string LOG_FILE_NAME = "tracelog.txt";
        public const string USERGUIDE_FILE_NAME = "TFSUserManagementUtil.docx";
    }

    public enum FileContentType
    {
        UsersToGroupsMapping,
        UsersList
    }
}
