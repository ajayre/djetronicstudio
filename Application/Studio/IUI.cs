using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJetronicStudio
{
    public interface IUI
    {
        event Action<object, ToolbarButton, bool> OnSetToolbarButtonState;
        event Action<object, StatusLabel, string> OnSetStatusLabelText;
        event Action<object, double> OnPercentageCompleted;

        IEnumerable<ToolbarButton> GetToolbarButtons();
        IEnumerable<StatusLabel> GetStatusLabels();

        void UIReady();
    }
}
