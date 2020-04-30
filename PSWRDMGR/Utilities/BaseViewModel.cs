using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PSWRDMGR.Utilities
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises a propertychanged event, allowing the view to be updated. Pass in your private property, new value, 
        /// and also the property name, but that's usually done for you.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">the private field that is used for "setting"</param>
        /// <param name="value">the new value of this property</param>
        /// <param name="propertyName">dont need to specify this usually, but the name of the property/field</param>
        public void RaisePropertyChanged<T>(ref T property, T value, [CallerMemberName] string propertyName = "")
        {
            property = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises a propertychanged event, allowing the view to be updated. Pass in your private property, new value,
        /// and a callback method containing the new value as a param if you want, and also the property name, but that's usually done for you.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">the private field that is used for "setting"</param>
        /// <param name="value">the new value of this property</param>
        /// <param name="propertyName">dont need to specify this usually, but the name of the property/field</param>
        /// <param name="callbackMethod">the method that gets called after and contains the value as a parameter</param>
        public void RaisePropertyChanged<T>(ref T property, T value, Action<T> callbackMethod, [CallerMemberName] string propertyName = "")
        {
            property = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            callbackMethod?.Invoke(value);
        }
        /// <summary>
        /// Raises a propertychanged event, allowing the view to be updated. Pass in your private property, new value,
        /// and a callback method, and also the property name, but that's usually done for you.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">the private field that is used for "setting"</param>
        /// <param name="value">the new value of this property</param>
        /// <param name="propertyName">dont need to specify this usually, but the name of the property/field</param>
        /// <param name="callbackMethod">the method that gets called after </param>
        public void RaisePropertyChanged<T>(ref T property, T value, Action callbackMethod, [CallerMemberName] string propertyName = "")
        {
            property = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            callbackMethod?.Invoke();
        }
    }
}
