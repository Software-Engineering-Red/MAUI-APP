using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace UndacApp
{
    public class Utils
    {
        /// <summary>
        /// Sets the property T on the given type and raises OnPropertyChanged.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <typeparam name="TType">The type of the enclosing type.</typeparam>
        /// <param name="storage">The backing field of the property.</param>
        /// <param name="value">The new value to set.</param>
        /// <param name="type">The enclosing type.</param>
        /// <param name="propertyName">The name of the property (passed to OnPropertyChanged)</param>
        /// <returns></returns>
        public static bool SetProperty<T, TType>(ref T storage, T value, TType type, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged<TType>(type, propertyName);
            return true;
        }

        /// <summary>
        /// Raises OnPropertyChanged on type T.
        /// </summary>
        protected static void OnPropertyChanged<T>(T type, [CallerMemberName] string propertyName = null)
        {
            var theEvent = type!.GetType().GetEvent("PropertyChanged");
            var backingField = type.GetType().GetField("PropertyChanged", BindingFlags.Instance | BindingFlags.NonPublic)!;
            var delegateInstance = backingField.GetValue(type) as PropertyChangedEventHandler;
            delegateInstance?.Invoke(type, new PropertyChangedEventArgs(propertyName));
        }
    }
}
