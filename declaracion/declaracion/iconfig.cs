using System;
using System.Collections.Generic;
using System.Text;
using SQLite.Net.Interop;

namespace declaracion
{
    public interface iconfig {

        string DirectorioDB { get; }

        ISQLitePlatform Plataforma { get; }

    }
}
