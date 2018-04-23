using System;
using System.Drawing;
using System.ComponentModel;

namespace WindowsApplication1
{
    public class GridRecentItem 
    {
        public GridRecentItem(string caption, Image itemImage, bool isPinned)
        {
            Caption = caption;
            Image = itemImage;
            IsPinned = isPinned;
        }

        private bool _IsPinned;
        public bool IsPinned
        {
            get { return _IsPinned; }
            set
            {
                _IsPinned = value;
            }
        }
        

        private string _Caption;
        public string Caption
        {
            get { return _Caption; }
            set { 
                _Caption = value;
            }
        }

        private Image _Image;
        public Image Image
        {
            get { return _Image; }
            set { 
                _Image = value;
            }
        }

        public object PinnedImage
        {
            get { return IsPinned; }
        }

     
    }
}
