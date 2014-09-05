using System;
using System.Collections.Generic;
using System.Linq;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework
{
    public static class Require
    {
        #region [ NotNull ]

        public static void RequireNotNull<T>(this T variable, string variableName = null, string message = null)
          where T : class
        {
            NotNull(variable, variableName, message);
        }
        public static void RequireNotNull<T>(this T variable, string variableName, Func<string> messageFunc)
          where T : class
        {
            NotNull(variable, variableName, messageFunc);
        }

        public static void NotNull<T>(T variable, string variableName = null, string message = null)
            where T : class
        {
            NotNull(variable, variableName, message == null ? (Func<string>)null : () => message);
        }

        public static void NotNull<T>(T variable, string variableName, Func<string> messageFunc)
          where T : class
        {
            if (variable == null)
                if (variableName == null)
                    if (messageFunc == null)
                        throw new ArgumentNullException();
                    else
                        throw new ArgumentNullException(messageFunc(), (Exception) null);
                else if (messageFunc == null)
                    throw new ArgumentNullException(variableName);
                else
                    throw new ArgumentNullException(variableName, messageFunc());
        }
        #endregion

        #region [ string: NotEmpty, NotWhitespace ]
        public static void RequireNotEmpty(this string variable, string variableName = null, string message = null)
        {
            NotEmpty(variable, variableName, message);
        }
        public static void RequireNotWhitespace(this string variable, string variableName = null, string message = null)
        {
            NotWhitespace(variable, variableName, message);
        }

        public static void NotEmpty(string variable, string variableName = null, string message = null)
        {
            RequireCondition(!string.IsNullOrEmpty(variable), variableName, message, Resources.ArgException_StringIsNotEmpty);
        }
        public static void NotWhitespace(string variable, string variableName = null, string message = null)
        {
            RequireCondition(!string.IsNullOrWhiteSpace(variable), variableName, message, Resources.ArgException_StringIsNotWhitespace);
        }
        #endregion

        #region [ Enum ]
        public static void RequireIsValidEnum<TEnum>(this TEnum enumValue, string variableName = null, string message = null)
          where TEnum : struct
        {
            IsValidEnum(enumValue, variableName, message);
        }

        public static void IsValidEnum<TEnum>(TEnum enumValue, string variableName = null, string message = null)
          where TEnum : struct
        {
            RequireCondition(
              Enum.IsDefined(typeof(TEnum), enumValue),
              variableName,
              message ?? FormattedResources.ArgException_EnumIsNotValid(variableName.AsString(Resources.ArgException_Value), typeof(TEnum).Name, enumValue.AsString()));
        }
        #endregion

        #region [ InRange, MinValue, MaxValue, MinMaxValid ]
        public static void RequireInRangeWithComparer<T>(this T variable, T minValue, T maxValue, string variableName = null, string message = null, IComparer<T> comparer = null)
        {
            InRangeWithComparer(variable, minValue, maxValue, variableName, message, comparer);
        }
        public static void RequireInRange<T>(this T variable, T minValue, T maxValue, string variableName = null, string message = null)
          where T : IComparable<T>
        {
            InRange(variable, minValue, maxValue, variableName, message);
        }

        public static void RequireMinValueWithComparer<T>(this T variable, T minValue, string variableName = null, string message = null, IComparer<T> comparer = null)
        {
            MinValueWithComparer(variable, minValue, variableName, message, comparer);
        }
        public static void RequireMinValue<T>(this T variable, T minValue, string variableName = null, string message = null)
          where T : IComparable<T>
        {
            MinValue(variable, minValue, variableName, message);
        }

        public static void RequireMaxValueWithComparer<T>(this T variable, T maxValue, string variableName = null, string message = null, IComparer<T> comparer = null)
        {
            MaxValueWithComparer(variable, maxValue, variableName, message, comparer);
        }
        public static void RequireMaxValue<T>(this T variable, T maxValue, string variableName = null, string message = null)
          where T : IComparable<T>
        {
            MaxValue(variable, maxValue, variableName, message);
        }

        public static void InRangeWithComparer<T>(T variable, T minValue, T maxValue, string variableName = null, string message = null, IComparer<T> comparer = null)
        {
            if (comparer == null) comparer = Comparer<T>.Default;
            if (comparer.Compare(minValue, variable) > 0 || comparer.Compare(maxValue, variable) < 0)
            {
                if (message == null)
                    message = FormattedResources.ArgOutOfRange_MinMaxMsg(variableName.AsString(Resources.ArgException_Value), variable.AsString(), minValue.AsString(), maxValue.AsString());
                throw new ArgumentOutOfRangeException(variableName, variable, message);
            }
        }
        public static void InRange<T>(T variable, T minValue, T maxValue, string variableName = null, string message = null)
          where T : IComparable<T>
        {
            if (minValue.CompareTo(variable) > 0 || maxValue.CompareTo(variable) < 0)
            {
                if (message == null)
                    message = FormattedResources.ArgOutOfRange_MinMaxMsg(variableName.AsString(Resources.ArgException_Value), variable.AsString(), minValue.AsString(), maxValue.AsString());
                throw new ArgumentOutOfRangeException(variableName, variable, message);
            }
        }

        public static void MinValueWithComparer<T>(T variable, T minValue, string variableName = null, string message = null, IComparer<T> comparer = null)
        {
            if (comparer == null) comparer = Comparer<T>.Default;
            if (comparer.Compare(minValue, variable) > 0)
            {
                if (message == null)
                    message = FormattedResources.ArgOutOfRange_MinMsg(variableName.AsString(Resources.ArgException_Value), variable.AsString(), minValue.AsString());
                throw new ArgumentOutOfRangeException(variableName, variable, message);
            }
        }
        public static void MinValue<T>(T variable, T minValue, string variableName = null, string message = null)
          where T : IComparable<T>
        {
            if (minValue.CompareTo(variable) > 0)
            {
                if (message == null)
                    message = FormattedResources.ArgOutOfRange_MinMsg(variableName.AsString(Resources.ArgException_Value), variable.AsString(), minValue.AsString());
                throw new ArgumentOutOfRangeException(variableName, variable, message);
            }
        }

        public static void MaxValueWithComparer<T>(T variable, T maxValue, string variableName = null, string message = null, IComparer<T> comparer = null)
        {
            if (comparer == null) comparer = Comparer<T>.Default;
            if (comparer.Compare(maxValue, variable) < 0)
            {
                if (message == null)
                    message = FormattedResources.ArgOutOfRange_MaxMsg(variableName.AsString(Resources.ArgException_Value), variable.AsString(), maxValue.AsString());
                throw new ArgumentOutOfRangeException(variableName, variable, message);
            }
        }
        public static void MaxValue<T>(T variable, T maxValue, string variableName = null, string message = null)
          where T : IComparable<T>
        {
            if (maxValue.CompareTo(variable) < 0)
            {
                if (message == null)
                    message = FormattedResources.ArgOutOfRange_MaxMsg(variableName.AsString(Resources.ArgException_Value), variable.AsString(), maxValue.AsString());
                throw new ArgumentOutOfRangeException(variableName, variable, message);
            }
        }

        public static void MinMaxValidWithComparer<T>(T minValue, T maxValue, string minVarName = null, string maxVarName = null, string message = null, IComparer<T> comparer = null)
        {
            if (comparer == null) comparer = Comparer<T>.Default;
            if (comparer.Compare(minValue, maxValue) > 0)
            {
                if (message == null)
                    message = FormattedResources.ArgOutOfRange_MinMaxValidMsg(
                      minVarName.AsString(Resources.ArgException_Min),
                      maxVarName.AsString(Resources.ArgException_Max),
                      minValue.AsString(),
                      maxValue.AsString());
                throw new ArgumentOutOfRangeException(maxVarName.AsString(Resources.ArgException_Max), maxValue, message);
            }
        }
        public static void MinMaxValidWithComparer<T>(T? minValue, T? maxValue, string minVarName = null, string maxVarName = null, string message = null, IComparer<T> comparer = null)
          where T : struct
        {
            if (!minValue.HasValue || !maxValue.HasValue) return;
            if (comparer == null) comparer = Comparer<T>.Default;
            if (comparer.Compare(minValue.Value, maxValue.Value) > 0)
            {
                if (message == null)
                    message = FormattedResources.ArgOutOfRange_MinMaxValidMsg(
                      minVarName.AsString(Resources.ArgException_Min),
                      maxVarName.AsString(Resources.ArgException_Max),
                      minValue.Value.AsString(),
                      maxValue.Value.AsString());
                throw new ArgumentOutOfRangeException(maxVarName.AsString(Resources.ArgException_Max), maxValue.Value, message);
            }
        }
        public static void MinMaxValid<T>(T minValue, T maxValue, string minVarName = null, string maxVarName = null, string message = null)
          where T : IComparable<T>
        {
            if (minValue.CompareTo(maxValue) > 0)
            {
                if (message == null)
                    message = FormattedResources.ArgOutOfRange_MinMaxValidMsg(
                      minVarName.AsString(Resources.ArgException_Min),
                      maxVarName.AsString(Resources.ArgException_Max),
                      minValue.AsString(),
                      maxValue.AsString());
                throw new ArgumentOutOfRangeException(maxVarName.AsString(Resources.ArgException_Max), maxValue, message);
            }
        }
        public static void MinMaxValid<T>(T? minValue, T? maxValue, string minVarName = null, string maxVarName = null, string message = null)
          where T : struct, IComparable<T>
        {
            if (!minValue.HasValue || !maxValue.HasValue) return;
            if (minValue.Value.CompareTo(maxValue.Value) > 0)
            {
                if (message == null)
                    message = FormattedResources.ArgOutOfRange_MinMaxValidMsg(
                      minVarName.AsString(Resources.ArgException_Min),
                      maxVarName.AsString(Resources.ArgException_Max),
                      minValue.Value.AsString(),
                      maxValue.Value.AsString());
                throw new ArgumentOutOfRangeException(maxVarName.AsString(Resources.ArgException_Max), maxValue.Value, message);
            }
        }
        #endregion

        #region [ Collections ]
        public static void RequireAllNonNull<T>(this IEnumerable<T> collection, string variableName = null, string message = null)
          where T : class
        {
            AllNonNull(collection, variableName, message);
        }
        public static void RequireAllNonNull<T>(this IEnumerable<T> collection, string variableName, Func<string> messageFunc)
          where T : class
        {
            AllNonNull(collection, variableName, messageFunc);
        }
        public static void RequireAllCondition<T>(this IEnumerable<T> collection, Func<T, bool> condition, string variableName = null, string message = null)
        {
            AllCondition(collection, condition, variableName, message);
        }
        public static void RequireAllCondition<T>(this IEnumerable<T> collection, Func<T, bool> condition, string variableName, Func<string> messageFunc)
        {
            AllCondition(collection, condition, variableName, messageFunc);
        }

        public static void AllNonNull<T>(IEnumerable<T> collection, string variableName = null, string message = null)
          where T : class
        {
            AllCondition(collection, t => t != null, variableName, message);
        }
        public static void AllNonNull<T>(IEnumerable<T> collection, string variableName, Func<string> messageFunc)
          where T : class
        {
            AllCondition(collection, t => t != null, variableName, messageFunc);
        }
        public static void AllCondition<T>(IEnumerable<T> collection, Func<T, bool> condition, string variableName = null, string message = null)
        {
            AllCondition(collection, condition, variableName, message != null ? () => message : (Func<string>)null);
        }
        public static void AllCondition<T>(IEnumerable<T> collection, Func<T, bool> condition, string variableName, Func<string> messageFunc)
        {
            RequireCondition(
              collection.All(condition),
              variableName,
              messageFunc ?? (() => FormattedResources.ArgException_NotAllElementsMatchCondition(variableName.AsString(Resources.ArgException_Collection))), 
              null);
        }
        #endregion

        #region [ Condition ]
        public static void RequireCondition(this bool condition, string variableName = null, string message = null, string conditionFormat = null)
        {
            Condition(condition, variableName, message, conditionFormat);
        }
        public static void RequireCondition(this bool condition, string variableName, Func<string> messageFunc, string conditionFormat)
        {
            Condition(condition, variableName, messageFunc, conditionFormat);
        }

        public static void Condition(bool condition, string variableName = null, string message = null, string conditionFormat = null)
        {
            Condition(condition, variableName, message != null ? () => message : (Func<string>)null, conditionFormat);
        }
        public static void Condition(bool condition, string variableName, Func<string> messageFunc, string conditionFormat)
        {
            if (!condition)
            {
                if (messageFunc == null)
                    if (conditionFormat != null)
                        messageFunc = () => string.Format(conditionFormat, variableName.AsString(Resources.ArgException_Value));
                    else
                        messageFunc = () => Resources.VarOrExprDoNotMeetCondition;
                throw new ArgumentException(messageFunc(), variableName.AsString(Resources.ArgException_Value));
            }
        }
        #endregion
    }
}
