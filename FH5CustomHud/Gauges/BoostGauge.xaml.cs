using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using FH5CustomHud.Core;

namespace FH5CustomHud.Gauges
{
    public partial class BoostGauge : Window
    {
        public BoostGauge()
        {
            InitializeComponent();
            setWidgetPosition();
            Globals.RPM_MAX.ValueChanged += () =>
            {
                
                this.Dispatcher.Invoke(new Action(() =>
                {
                    if (Globals.RPM_MAX.Value == 0)
                    {
                        if (Globals.AutoHide)
                        {
                            Meter_RPM.Visibility = Visibility.Hidden;
                            Needle_RPM.Visibility = Visibility.Hidden;
                        }
                        
                    }
                    else
                    {
                        
                        Meter_RPM.Visibility = Visibility.Visible;
                        Needle_RPM.Visibility = Visibility.Visible;
                    }
                }));
            };
            Globals.BOOST.ValueChanged += () =>
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    double cache = Globals.BOOST.Value;
                    //tb_PSI.Text = Convert.ToString((int)Globals.BOOST.Value);
                    double calcx = 270.0 / 60;
                    if (cache < 0)
                    {
                        cache = 0;
                    }
                    double angle = cache * calcx;
                    //debug_psi_angle.Text = angle.ToString();
                    
                    Needle_RPM.RenderTransformOrigin = new Point(.5,1);
                    Needle_RPM.RenderTransform = new RotateTransform(angle - 180);
                }));
            };
        }
        public void setWidgetPosition()
        {
            int screenWidth = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
            int screenHeight = (int)System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Left = screenWidth - (screenWidth * 0.13);
            this.Top = screenHeight - (screenHeight * 0.57);
        }

        private void BoostGauge_OnDeactivated(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Topmost = true;
        }

        private void BoostGauge_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}