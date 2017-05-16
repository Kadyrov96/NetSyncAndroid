using System.Net.Security;

namespace NetSync_Mobile
{
    class ProfileProcessor
    {
        IStreamHandler streamHandler;
        Synchroniser syncService;

        public ProfileProcessor(SslStream sslStream)
        {
            streamHandler = new IOStreamHandler(sslStream);
        }

        public void ProcessingProfile(SyncProfile profile)
        {
            //streamHandler.SendString(profile.ProfileName);

            syncService = new Synchroniser(new FolderHandler(profile.ProfileSyncFolderPath));
            syncService.Synchronise(streamHandler);
            SyncProfilesHandler.UpdateProfile(profile);

        }
    }
}
