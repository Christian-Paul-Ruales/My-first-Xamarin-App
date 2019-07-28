using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using SQLite.Net.Interop;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(declaracion.iOS.config))]

namespace declaracion.iOS
{
    class config : iconfig
    {
        private string directoriodb;
        private ISQLitePlatform plataforma;
        public string DirectorioDB {
            get {
                if (string.IsNullOrEmpty(directoriodb)) {
                    var directorio = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    directoriodb = System.IO.Path.Combine(directorio,"..","Library");
                        }
                return directoriodb;
            }

           
        }
        public ISQLitePlatform Plataforma {
            get {
                if (plataforma==null) {
                    plataforma = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();

                }
                return plataforma;
               
            }
            
        }
    }
}