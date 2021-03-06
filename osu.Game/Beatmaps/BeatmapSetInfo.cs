﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace osu.Game.Beatmaps
{
    public class BeatmapSetInfo
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int? OnlineBeatmapSetID { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public BeatmapMetadata Metadata { get; set; }

        [NotNull, ForeignKey(typeof(BeatmapMetadata))]
        public int BeatmapMetadataID { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<BeatmapInfo> Beatmaps { get; set; }

        [Ignore]
        public BeatmapSetOnlineInfo OnlineInfo { get; set; }

        public double MaxStarDifficulty => Beatmaps.Max(b => b.StarDifficulty);

        [Indexed]
        public bool DeletePending { get; set; }

        public string Hash { get; set; }

        public string StoryboardFile => Files.FirstOrDefault(f => f.Filename.EndsWith(".osb"))?.Filename;

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<BeatmapSetFileInfo> Files { get; set; }

        public bool Protected { get; set; }
    }
}
