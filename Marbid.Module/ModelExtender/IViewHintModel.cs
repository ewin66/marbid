using DevExpress.ExpressApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marbid.Module.ModelExtender
{
  public interface IViewHintModel : IModelNode
  {
    string Hint { get; set; }
  }
}
