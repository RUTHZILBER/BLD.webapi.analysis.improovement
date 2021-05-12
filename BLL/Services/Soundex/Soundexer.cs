﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.Requests;

namespace BLL.Services.Soundex
{
    class Soundexer
    {
        public const string SOUNDEX_ENDPOINT = "/service/soundex";
        private static readonly Service<SoundexRequest> Service = new Service<SoundexRequest>(SOUNDEX_ENDPOINT);

        public static List<List<uint>> Soundex(string[] words)
        {
            return Service.GetService<List<List<uint>>>(new SoundexRequest { words = words });
        }
    }
}
