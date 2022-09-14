using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InterTwitter.Controls.StateContainer
{
    [ContentProperty("Conditions")]
    public class StateContainer : ContentView
    {
        #region -- Public properties --

        public List<StateCondition> Conditions { get; set; } = new List<StateCondition>();

        public static readonly BindableProperty StateProperty = BindableProperty.Create(
            propertyName: nameof(State),
            returnType: typeof(object),
            declaringType: typeof(StateContainer),
            propertyChanged: OnStatePropertyChanged);

        public object State
        {
            get => GetValue(StateProperty);
            set => SetValue(StateProperty, value);
        }

        #endregion

        #region --- Private helpers ---

        private static void OnStatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is StateContainer parent)
            {
                parent.ChooseStateAsync(newValue);
            }
        }

        private Task ChooseStateAsync(object newValue)
        {
            var currentCondition = Conditions?.FirstOrDefault(condition => condition?.State?.ToString() == newValue?.ToString());

            var view = currentCondition?.Content;

            Content = view;

            return Task.CompletedTask;
        }

        #endregion
    }
}
