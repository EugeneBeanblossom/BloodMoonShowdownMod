using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using static XUiC_DropDown;

namespace BloodMoonShowdownMod.Network
{
    public class NetPackageLeaderboard : NetPackage
    {
        private int _entryCount;
        private LeaderboardEntry[] entries;

        public NetPackageLeaderboard Setup()
        {
            _entryCount = ShowdownLeaderboard.Instance.Players.Count;
            return this;
        }

        public override int GetLength()
        {
            // Max steam name length is 32 chars
            // 3 int32
            // multiply by array size
            return 44 * _entryCount;
        }

        public override void ProcessPackage(World _world, GameManager _callbacks)
        {
            if (!SingletonMonoBehaviour<ConnectionManager>.Instance.IsServer)
            {
                foreach(var entry in entries)
                {
                    ShowdownLeaderboard.Instance.UpdateFromServer(entry);
                }
            }
        }

        public override void read(PooledBinaryReader _reader)
        {
            int length = _reader.ReadInt32();
            entries = new LeaderboardEntry[length];
            for(int i = 0; i < length; i++)
            {
                entries[i] = new LeaderboardEntry(); ;
                entries[i].EntityId = _reader.ReadInt32();
                entries[i].Kills = _reader.ReadInt32();
                entries[i].SteamId = _reader.ReadString();
            }
        }

        public override void write(PooledBinaryWriter _writer)
        {
            base.write(_writer);
            _writer.Write(_entryCount);
            foreach (var entry in ShowdownLeaderboard.Instance.Players)
            {
                _writer.Write(entry.Value.EntityId);
                _writer.Write(entry.Value.Kills);
                _writer.Write(entry.Value.SteamId);
            }
        }
    }
}
