namespace NetSync_Mobile
{
    internal interface IStreamHandler
    {
        void ReceiveData(string savingFolderPath);
        void SendData(string filePath);

        string ReceiveString();
        int ReceiveNum();

        void SendNum();
        void SendString(string stringParam);
    }
}