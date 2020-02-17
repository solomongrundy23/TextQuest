using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public static class TextsBase
    {
        public class Dialogs
        {
            public interface IDialog
            {
                void Say();
            }
        }
    }
}
