using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaWeb.Domain.Utility
{
    public static class CommonConstants
    {
        public class Header
        {
            public const string CurrentUser = "CurrentUser";
        }

        public class Permissions
        {
            // User Permissions
            public const string USER_PERMISSION = "USER_PERMISSION";
            public const string ADD_USER_PERMISSION = "ADD_USER_PERMISSION";
            public const string UPDATE_USER_PERMISSION = "UPDATE_USER_PERMISSION";
            public const string DELETE_USER_PERMISSION = "DELETE_USER_PERMISSION";
            public const string VIEW_USER_PERMISSION = "VIEW_USER_PERMISSION";

            // Role Permissions
            public const string ROLE_PERMISSION = "ROLE_PERMISSION";
            public const string ADD_ROLE_PERMISSION = "ADD_ROLE_PERMISSION";
            public const string UPDATE_ROLE_PERMISSION = "UPDATE_ROLE_PERMISSION";
            public const string DELETE_ROLE_PERMISSION = "DELETE_ROLE_PERMISSION";
            public const string VIEW_ROLE_PERMISSION = "VIEW_ROLE_PERMISSION";

            // Manga Permissions
            public const string MANGA_PERMISSION = "MANGA_PERMISSION";
            public const string ADD_MANGA_PERMISSION = "ADD_MANGA_PERMISSION";
            public const string UPDATE_MANGA_PERMISSION = "UPDATE_MANGA_PERMISSION";
            public const string DELETE_MANGA_PERMISSION = "DELETE_MANGA_PERMISSION";
            public const string VIEW_MANGA_PERMISSION = "VIEW_MANGA_PERMISSION";

            // Chapter Permissions
            public const string CHAPTER_PERMISSION = "CHAPTER_PERMISSION";
            public const string ADD_CHAPTER_PERMISSION = "ADD_CHAPTER_PERMISSION";
            public const string UPDATE_CHAPTER_PERMISSION = "UPDATE_CHAPTER_PERMISSION";
            public const string DELETE_CHAPTER_PERMISSION = "DELETE_CHAPTER_PERMISSION";
            public const string VIEW_CHAPTER_PERMISSION = "VIEW_CHAPTER_PERMISSION";

            // Author Permissions
            public const string AUTHOR_PERMISSION = "AUTHOR_PERMISSION";
            public const string ADD_AUTHOR_PERMISSION = "ADD_AUTHOR_PERMISSION";
            public const string UPDATE_AUTHOR_PERMISSION = "UPDATE_AUTHOR_PERMISSION";
            public const string DELETE_AUTHOR_PERMISSION = "DELETE_AUTHOR_PERMISSION";
            public const string VIEW_AUTHOR_PERMISSION = "VIEW_AUTHOR_PERMISSION";

            // Tag Permissions
            public const string TAG_PERMISSION = "TAG_PERMISSION";
            public const string ADD_TAG_PERMISSION = "ADD_TAG_PERMISSION";
            public const string UPDATE_TAG_PERMISSION = "UPDATE_TAG_PERMISSION";
            public const string DELETE_TAG_PERMISSION = "DELETE_TAG_PERMISSION";
            public const string VIEW_TAG_PERMISSION = "VIEW_TAG_PERMISSION";

            // Review Permissions
            public const string REVIEW_PERMISSION = "REVIEW_PERMISSION";
            public const string ADD_REVIEW_PERMISSION = "ADD_REVIEW_PERMISSION";
            public const string UPDATE_REVIEW_PERMISSION = "UPDATE_REVIEW_PERMISSION";
            public const string DELETE_REVIEW_PERMISSION = "DELETE_REVIEW_PERMISSION";
            public const string VIEW_REVIEW_PERMISSION = "VIEW_REVIEW_PERMISSION";

            // Image Permissions
            public const string IMAGE_PERMISSION = "IMAGE_PERMISSION";
            public const string ADD_IMAGE_PERMISSION = "ADD_IMAGE_PERMISSION";
            public const string UPDATE_IMAGE_PERMISSION = "UPDATE_IMAGE_PERMISSION";
            public const string DELETE_IMAGE_PERMISSION = "DELETE_IMAGE_PERMISSION";
            public const string VIEW_IMAGE_PERMISSION = "VIEW_IMAGE_PERMISSION";
        }
    }
}