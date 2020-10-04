using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PSWRDMGR.Utilities
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises a propertychanged event, allowing the view to be updated. Pass in your private property, new value, 
        /// can also pass the property name but that's done for you.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">the private field that is used for "setting"</param>
        /// <param name="newValue">the new value of this property</param>
        /// <param name="propertyName">dont need to specify this, but the name of the property/field</param>
        public void RaisePropertyChanged<T>(ref T property, T newValue, [CallerMemberName] string propertyName = "")
        {
            property = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises a propertychanged event, allowing the view to be updated. Pass in your private property, new value,
        /// and a callback method, can also pass the property name but that's done for you.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">the private field that is used for "setting"</param>
        /// <param name="value">the new value of this property</param>
        /// <param name="propertyName">dont need to specify this usually, but the name of the property/field</param>
        /// <param name="callbackMethod">the method that gets called after property changed</param>
        public void RaisePropertyChanged<T>(ref T property, T newValue, Action callbackMethod, [CallerMemberName] string propertyName = "")
        {
            property = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            callbackMethod?.Invoke();
        }

        /// <summary>
        /// Raises a propertychanged event, allowing the view to be updated. Pass in your private property, new value,
        /// and a callback method containing the new value as a param if you want, can also pass the property name but that's done for you.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">the private field that is used for "setting"</param>
        /// <param name="newValue">the new value of this property</param>
        /// <param name="propertyName">dont need to specify this usually, but the name of the property/field</param>
        /// <param name="callbackMethod">The method that gets called after property changed, and contains the new value as a parameter</param>
        public void RaisePropertyChanged<T>(ref T property, T newValue, Action<T> callbackMethod, [CallerMemberName] string propertyName = "")
        {
            property = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            callbackMethod?.Invoke(newValue);
        }
    }
}
