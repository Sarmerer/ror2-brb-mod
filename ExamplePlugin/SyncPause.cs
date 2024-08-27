using R2API.Networking.Interfaces;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

namespace BRB
{
    internal class SyncPause : INetMessage
    {
        bool isPaused;

        public SyncPause() { }

        public SyncPause(bool isPaused)
        {
            this.isPaused = isPaused;
        }

        public void Deserialize(NetworkReader reader)
        {
            isPaused = reader.ReadBoolean();
        }

        public void OnReceived()
        {
            
            BRB.SetGamePauseState(isPaused);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(isPaused);
        }
    }
}
