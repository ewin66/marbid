using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;

namespace Marbid.Win
{
  public partial class MarbidSplash : SplashScreen
  {
    public MarbidSplash()
    {
      InitializeComponent();
    }

    #region Overrides

    public override void ProcessCommand(Enum cmd, object arg)
    {
      base.ProcessCommand(cmd, arg);
    }

    #endregion

    public enum SplashScreenCommand
    {
    }

    private void marqueeProgressBarControl1_EditValueChanged(object sender, EventArgs e)
    {

    }

    private void MarbidSplash_Load(object sender, EventArgs e)
    {
      labelControl3.Text = string.Format("Marbid Desktop Ver. {0}", Application.ProductVersion);
    }
  }
}