using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Runtime.CompilerServices;

namespace SelfMonitoringApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RatingSlider : Grid
    {
        public string MinValueText { get; set; } = "0";

        public string Title
        {


            set
            { 
                
            }
        }


        public RatingSlider()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}