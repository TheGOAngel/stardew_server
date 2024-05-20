﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewModdingAPI;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Locations;
using StardewValley.SDKs;
using StardewValley.WorldMaps;

namespace JunimoServer.Util
{
    public static class ModHelperExtensions
    {
        public static Multiplayer GetMultiplayer(this IModHelper helper)
        {
            return helper.Reflection.GetField<Multiplayer>(typeof(Game1), "multiplayer").GetValue();
        }

        public static Farmer FindPlayerIdByFarmerNameOrUserName(this IModHelper helper, string name)
        {
            return Game1
                .getAllFarmers()
                .FirstOrDefault(farmer =>
                    farmer.Name == name || helper.GetFarmerUserNameById(farmer.UniqueMultiplayerID) == name
                );
        }

        public static string GetFarmerUserNameById(this IModHelper helper, long id)
        {
            return helper.GetMultiplayer().getUserName(id);
        }

        public static string GetFarmerNameById(this IModHelper helper, long id)
        {
            return Game1
                .getAllFarmers()
                .FirstOrDefault(farmer => farmer.UniqueMultiplayerID == id)?.Name;
        }

        public static void SendPublicMessage(this IModHelper helper, string msg)
        {
            helper.GetMultiplayer()
                .sendChatMessage(LocalizedContentManager.CurrentLanguageCode, msg, Multiplayer.AllPlayers);
        }

        public static void SendPrivateMessage(this IModHelper helper, long uniqueMultiplayerId, string msg)
        {
            helper.GetMultiplayer()
                .sendChatMessage(LocalizedContentManager.CurrentLanguageCode, msg, uniqueMultiplayerId);
        }

        public static int GetCurrentNumCabins(this IModHelper helper)
        {
            return helper.Reflection.GetMethod(Game1.server, "cabins")
                .Invoke<IEnumerable<Cabin>>().ToList().Count;
        }

        public static bool IsNetworkingReady(this IModHelper helper)
        {
            var sdk = helper.Reflection.GetField<SDKHelper>(typeof(Program), "_sdk").GetValue();
            return sdk != null && sdk.Networking != null;
        }

        public static long GetOwnerPlayerId(this IModHelper helper)
        {
            if (Game1.activeClickableMenu == null)
            {
                return -1;
            }

            helper.Reflection.GetMethod(Game1.activeClickableMenu, "okClicked").Invoke();
            return ((Cabin)Game1.getFarm().buildings.First(building => building.isCabin).indoors.Value).owner.UniqueMultiplayerID;
        }

        public static void WriteServerJsonFile(this IDataHelper dataHelper, string path, object data)
        {
            path = GetServerDataPath(path);

            if (data != null)
            {
                Stream stream = File.Create(path);
                stream.Write(new UTF8Encoding(true).GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented)));
                stream.Dispose();
            }
            else
            {
                File.Delete(path);
            }
        }

        public static void WriteServerTextureFile(this IDataHelper dataHelper, string path, Texture2D texture)
        {
            path = GetServerDataPath(path);

            if (texture != null)
            {
                Stream stream = File.Create(path);
                texture.SaveAsPng(stream, texture.Width, texture.Height);
                stream.Dispose();
            }
            else
            {
                File.Delete(path);
            }
        }

        /// <summary>
        /// The directory path where all saves are stored.
        /// </summary>
        private static string GetServerDataPath(string path)
        {
            if (!PathUtilities.IsSafeRelativePath(path))
            {
                throw new InvalidOperationException("You must call the function with a relative path (without directory climbing).");
            }

            path = Path.Combine(Program.GetAppDataFolder("Server"), path);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            return path;
        }

        //private static void AssertSlug(string key, string paramName)
        //{
        //    if (!PathUtilities.IsSlug(key))
        //    {
        //        throw new ArgumentException("The data key is invalid (keys must only contain letters, numbers, underscores, periods, or hyphens).", paramName);
        //    }
        //}
    }
}