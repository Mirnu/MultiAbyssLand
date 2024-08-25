using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.World
{
    public class WorldSaver {
        private Tilemap _map;
        private List<TileData> _tiles = new List<TileData>();

        public WorldSaver(Tilemap _) {
            _map = _;
        }

        public void Save() {
            BinaryFormatter bf = new BinaryFormatter();
            // Пока что в json но если сигма прогер считает что лучше в dat значит в dat 
            FileStream file = File.Create(Application.persistentDataPath
              + "/MySaveWorldData.json");
            _map.CompressBounds();
            for (int i = _map.cellBounds.xMin; i < _map.cellBounds.xMax; i++) {
                for (int j = _map.cellBounds.yMin; j < _map.cellBounds.yMax; j++) {
                    if(_map.GetTile(new Vector3Int(i, j)) != null) {
                        _tiles.Add(new TileData(_map.GetTile(new Vector3Int(i, j)), new Vector3Int(i, j)));
                    }
                }
            }
            var s = "\n";
            _tiles.ForEach(x => s += JsonUtility.ToJson(x) + "\n");
            s += "---\n";
            //_handler._damagableTiles.ForEach(x => s += JsonUtility.ToJson(x) + "\n");
            bf.Serialize(file, s);
            file.Close();
        }

        public void Load() {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                  File.Open(Application.persistentDataPath
                  + "/MySaveWorldData.json", FileMode.Open);
            List<TileData> _ = new List<TileData>();
            var l = (string)bf.Deserialize(file);
            l.Split("---")[0].Split("\n").ToList().ForEach(x => {
                if(x != "") {
                    _.Add(JsonUtility.FromJson<TileData>(x));
            }});
            l.Split("---")[1].Split("\n").ToList().ForEach(x => {
                if(x != "") {
                    //_handler._damagableTiles.Add(JsonUtility.FromJson<DmgTile>(x));
            }});
            file.Close();
        }

        private void fill(Tilemap tmp, List<TileData> data) {
            data.ForEach(x => tmp.SetTile(x.Pos + new Vector3Int(15, 5, 0), x.Tile));
        }
    }
}

    [Serializable]
    public class TileData {
        public Vector3Int Pos;
        public TileBase Tile;
        public TileData(TileBase _tile, Vector3Int _pos) {
            Pos = _pos;
            Tile = _tile;
        }
    }
