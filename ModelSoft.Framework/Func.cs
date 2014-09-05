 

namespace ModelSoft.Framework
{
    using global::System;

    /// <summary>
    /// Encapsulates a method that has 9 parameters and returns a value of the type specified by the TResult parameter.
    /// </summary>
    /// <typeparam name="T1">The type of the 1th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the 2th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the 3th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the 4th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the 5th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T6">The type of the 6th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T7">The type of the 7th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T8">The type of the 8th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T9">The type of the 9th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <param name="item1">The 1th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item2">The 2th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item3">The 3th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item4">The 4th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item5">The 5th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item6">The 6th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item7">The 7th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item8">The 8th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item9">The 9th parameter of the method that this delegate encapsulates.</param>
    /// <returns>The return value of the method that this delegate encapsulates.</returns>
    public delegate TResult Func<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, out TResult>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9);
    /// <summary>
    /// Encapsulates a method that has 10 parameters and returns a value of the type specified by the TResult parameter.
    /// </summary>
    /// <typeparam name="T1">The type of the 1th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the 2th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the 3th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the 4th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the 5th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T6">The type of the 6th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T7">The type of the 7th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T8">The type of the 8th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T9">The type of the 9th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T10">The type of the 10th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <param name="item1">The 1th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item2">The 2th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item3">The 3th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item4">The 4th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item5">The 5th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item6">The 6th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item7">The 7th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item8">The 8th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item9">The 9th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item10">The 10th parameter of the method that this delegate encapsulates.</param>
    /// <returns>The return value of the method that this delegate encapsulates.</returns>
    public delegate TResult Func<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, out TResult>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10);
    /// <summary>
    /// Encapsulates a method that has 11 parameters and returns a value of the type specified by the TResult parameter.
    /// </summary>
    /// <typeparam name="T1">The type of the 1th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the 2th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the 3th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the 4th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the 5th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T6">The type of the 6th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T7">The type of the 7th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T8">The type of the 8th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T9">The type of the 9th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T10">The type of the 10th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T11">The type of the 11th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <param name="item1">The 1th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item2">The 2th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item3">The 3th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item4">The 4th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item5">The 5th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item6">The 6th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item7">The 7th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item8">The 8th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item9">The 9th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item10">The 10th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item11">The 11th parameter of the method that this delegate encapsulates.</param>
    /// <returns>The return value of the method that this delegate encapsulates.</returns>
    public delegate TResult Func<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, out TResult>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11);
    /// <summary>
    /// Encapsulates a method that has 12 parameters and returns a value of the type specified by the TResult parameter.
    /// </summary>
    /// <typeparam name="T1">The type of the 1th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the 2th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the 3th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the 4th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the 5th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T6">The type of the 6th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T7">The type of the 7th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T8">The type of the 8th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T9">The type of the 9th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T10">The type of the 10th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T11">The type of the 11th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T12">The type of the 12th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <param name="item1">The 1th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item2">The 2th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item3">The 3th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item4">The 4th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item5">The 5th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item6">The 6th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item7">The 7th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item8">The 8th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item9">The 9th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item10">The 10th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item11">The 11th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item12">The 12th parameter of the method that this delegate encapsulates.</param>
    /// <returns>The return value of the method that this delegate encapsulates.</returns>
    public delegate TResult Func<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, out TResult>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12);
    /// <summary>
    /// Encapsulates a method that has 13 parameters and returns a value of the type specified by the TResult parameter.
    /// </summary>
    /// <typeparam name="T1">The type of the 1th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the 2th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the 3th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the 4th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the 5th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T6">The type of the 6th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T7">The type of the 7th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T8">The type of the 8th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T9">The type of the 9th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T10">The type of the 10th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T11">The type of the 11th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T12">The type of the 12th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T13">The type of the 13th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <param name="item1">The 1th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item2">The 2th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item3">The 3th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item4">The 4th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item5">The 5th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item6">The 6th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item7">The 7th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item8">The 8th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item9">The 9th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item10">The 10th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item11">The 11th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item12">The 12th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item13">The 13th parameter of the method that this delegate encapsulates.</param>
    /// <returns>The return value of the method that this delegate encapsulates.</returns>
    public delegate TResult Func<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, out TResult>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13);
    /// <summary>
    /// Encapsulates a method that has 14 parameters and returns a value of the type specified by the TResult parameter.
    /// </summary>
    /// <typeparam name="T1">The type of the 1th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the 2th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the 3th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the 4th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the 5th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T6">The type of the 6th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T7">The type of the 7th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T8">The type of the 8th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T9">The type of the 9th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T10">The type of the 10th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T11">The type of the 11th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T12">The type of the 12th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T13">The type of the 13th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T14">The type of the 14th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <param name="item1">The 1th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item2">The 2th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item3">The 3th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item4">The 4th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item5">The 5th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item6">The 6th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item7">The 7th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item8">The 8th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item9">The 9th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item10">The 10th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item11">The 11th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item12">The 12th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item13">The 13th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item14">The 14th parameter of the method that this delegate encapsulates.</param>
    /// <returns>The return value of the method that this delegate encapsulates.</returns>
    public delegate TResult Func<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, out TResult>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14);
    /// <summary>
    /// Encapsulates a method that has 15 parameters and returns a value of the type specified by the TResult parameter.
    /// </summary>
    /// <typeparam name="T1">The type of the 1th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the 2th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the 3th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the 4th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the 5th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T6">The type of the 6th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T7">The type of the 7th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T8">The type of the 8th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T9">The type of the 9th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T10">The type of the 10th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T11">The type of the 11th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T12">The type of the 12th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T13">The type of the 13th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T14">The type of the 14th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T15">The type of the 15th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <param name="item1">The 1th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item2">The 2th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item3">The 3th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item4">The 4th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item5">The 5th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item6">The 6th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item7">The 7th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item8">The 8th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item9">The 9th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item10">The 10th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item11">The 11th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item12">The 12th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item13">The 13th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item14">The 14th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item15">The 15th parameter of the method that this delegate encapsulates.</param>
    /// <returns>The return value of the method that this delegate encapsulates.</returns>
    public delegate TResult Func<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, in T15, out TResult>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15);
    /// <summary>
    /// Encapsulates a method that has 16 parameters and returns a value of the type specified by the TResult parameter.
    /// </summary>
    /// <typeparam name="T1">The type of the 1th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the 2th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the 3th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the 4th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the 5th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T6">The type of the 6th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T7">The type of the 7th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T8">The type of the 8th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T9">The type of the 9th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T10">The type of the 10th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T11">The type of the 11th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T12">The type of the 12th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T13">The type of the 13th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T14">The type of the 14th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T15">The type of the 15th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T16">The type of the 16th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <param name="item1">The 1th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item2">The 2th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item3">The 3th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item4">The 4th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item5">The 5th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item6">The 6th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item7">The 7th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item8">The 8th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item9">The 9th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item10">The 10th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item11">The 11th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item12">The 12th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item13">The 13th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item14">The 14th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item15">The 15th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item16">The 16th parameter of the method that this delegate encapsulates.</param>
    /// <returns>The return value of the method that this delegate encapsulates.</returns>
    public delegate TResult Func<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, in T15, in T16, out TResult>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16);
    /// <summary>
    /// Encapsulates a method that has 17 parameters and returns a value of the type specified by the TResult parameter.
    /// </summary>
    /// <typeparam name="T1">The type of the 1th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the 2th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the 3th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the 4th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the 5th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T6">The type of the 6th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T7">The type of the 7th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T8">The type of the 8th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T9">The type of the 9th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T10">The type of the 10th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T11">The type of the 11th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T12">The type of the 12th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T13">The type of the 13th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T14">The type of the 14th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T15">The type of the 15th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T16">The type of the 16th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T17">The type of the 17th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <param name="item1">The 1th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item2">The 2th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item3">The 3th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item4">The 4th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item5">The 5th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item6">The 6th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item7">The 7th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item8">The 8th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item9">The 9th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item10">The 10th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item11">The 11th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item12">The 12th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item13">The 13th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item14">The 14th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item15">The 15th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item16">The 16th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item17">The 17th parameter of the method that this delegate encapsulates.</param>
    /// <returns>The return value of the method that this delegate encapsulates.</returns>
    public delegate TResult Func<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, in T15, in T16, in T17, out TResult>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, T17 item17);
    /// <summary>
    /// Encapsulates a method that has 18 parameters and returns a value of the type specified by the TResult parameter.
    /// </summary>
    /// <typeparam name="T1">The type of the 1th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the 2th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the 3th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the 4th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the 5th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T6">The type of the 6th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T7">The type of the 7th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T8">The type of the 8th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T9">The type of the 9th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T10">The type of the 10th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T11">The type of the 11th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T12">The type of the 12th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T13">The type of the 13th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T14">The type of the 14th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T15">The type of the 15th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T16">The type of the 16th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T17">The type of the 17th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T18">The type of the 18th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <param name="item1">The 1th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item2">The 2th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item3">The 3th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item4">The 4th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item5">The 5th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item6">The 6th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item7">The 7th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item8">The 8th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item9">The 9th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item10">The 10th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item11">The 11th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item12">The 12th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item13">The 13th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item14">The 14th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item15">The 15th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item16">The 16th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item17">The 17th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item18">The 18th parameter of the method that this delegate encapsulates.</param>
    /// <returns>The return value of the method that this delegate encapsulates.</returns>
    public delegate TResult Func<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, in T15, in T16, in T17, in T18, out TResult>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, T17 item17, T18 item18);
    /// <summary>
    /// Encapsulates a method that has 19 parameters and returns a value of the type specified by the TResult parameter.
    /// </summary>
    /// <typeparam name="T1">The type of the 1th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the 2th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the 3th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the 4th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the 5th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T6">The type of the 6th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T7">The type of the 7th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T8">The type of the 8th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T9">The type of the 9th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T10">The type of the 10th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T11">The type of the 11th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T12">The type of the 12th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T13">The type of the 13th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T14">The type of the 14th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T15">The type of the 15th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T16">The type of the 16th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T17">The type of the 17th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T18">The type of the 18th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T19">The type of the 19th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <param name="item1">The 1th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item2">The 2th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item3">The 3th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item4">The 4th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item5">The 5th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item6">The 6th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item7">The 7th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item8">The 8th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item9">The 9th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item10">The 10th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item11">The 11th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item12">The 12th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item13">The 13th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item14">The 14th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item15">The 15th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item16">The 16th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item17">The 17th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item18">The 18th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item19">The 19th parameter of the method that this delegate encapsulates.</param>
    /// <returns>The return value of the method that this delegate encapsulates.</returns>
    public delegate TResult Func<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, in T15, in T16, in T17, in T18, in T19, out TResult>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, T17 item17, T18 item18, T19 item19);
    /// <summary>
    /// Encapsulates a method that has 20 parameters and returns a value of the type specified by the TResult parameter.
    /// </summary>
    /// <typeparam name="T1">The type of the 1th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the 2th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the 3th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the 4th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the 5th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T6">The type of the 6th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T7">The type of the 7th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T8">The type of the 8th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T9">The type of the 9th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T10">The type of the 10th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T11">The type of the 11th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T12">The type of the 12th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T13">The type of the 13th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T14">The type of the 14th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T15">The type of the 15th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T16">The type of the 16th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T17">The type of the 17th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T18">The type of the 18th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T19">The type of the 19th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T20">The type of the 20th parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <param name="item1">The 1th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item2">The 2th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item3">The 3th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item4">The 4th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item5">The 5th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item6">The 6th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item7">The 7th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item8">The 8th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item9">The 9th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item10">The 10th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item11">The 11th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item12">The 12th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item13">The 13th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item14">The 14th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item15">The 15th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item16">The 16th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item17">The 17th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item18">The 18th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item19">The 19th parameter of the method that this delegate encapsulates.</param>
    /// <param name="item20">The 20th parameter of the method that this delegate encapsulates.</param>
    /// <returns>The return value of the method that this delegate encapsulates.</returns>
    public delegate TResult Func<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, in T15, in T16, in T17, in T18, in T19, in T20, out TResult>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, T17 item17, T18 item18, T19 item19, T20 item20);

    public static class FuncExtensions
    {
        public static TResult EvalOr<TResult>(this Func<TResult> func, TResult defaultValue = default(TResult))
        {
            return func.EvalOr(() => defaultValue);
        }
        public static TResult EvalOr<TResult>(this Func<TResult> func, Func<TResult> otherwiseFunc)
        {
            if (func == null)
                if (otherwiseFunc == null)
                    return default(TResult);
                else
                    return otherwiseFunc(); 
            return func();
        }

        public static TResult EvalOr<T1, TResult>(this Func<T1, TResult> func, T1 item1, TResult defaultValue = default(TResult))
        {
            return func.EvalOr(item1, delegate { return defaultValue; });
        }
        public static TResult EvalOr<T1, TResult>(this Func<T1, TResult> func, T1 item1, Func<T1, TResult> otherwiseFunc)
        {
            if (func == null)
                if (otherwiseFunc == null)
                    return default(TResult);
                else
                    return otherwiseFunc(item1); 
            return func(item1);
        }

        public static TResult EvalOr<T1, T2, TResult>(this Func<T1, T2, TResult> func, T1 item1, T2 item2, TResult defaultValue = default(TResult))
        {
            return func.EvalOr(item1, item2, delegate { return defaultValue; });
        }
        public static TResult EvalOr<T1, T2, TResult>(this Func<T1, T2, TResult> func, T1 item1, T2 item2, Func<T1, T2, TResult> otherwiseFunc)
        {
            if (func == null)
                if (otherwiseFunc == null)
                    return default(TResult);
                else
                    return otherwiseFunc(item1, item2); 
            return func(item1, item2);
        }

        public static TResult EvalOr<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func, T1 item1, T2 item2, T3 item3, TResult defaultValue = default(TResult))
        {
            return func.EvalOr(item1, item2, item3, delegate { return defaultValue; });
        }
        public static TResult EvalOr<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func, T1 item1, T2 item2, T3 item3, Func<T1, T2, T3, TResult> otherwiseFunc)
        {
            if (func == null)
                if (otherwiseFunc == null)
                    return default(TResult);
                else
                    return otherwiseFunc(item1, item2, item3); 
            return func(item1, item2, item3);
        }

        public static TResult EvalOr<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, TResult defaultValue = default(TResult))
        {
            return func.EvalOr(item1, item2, item3, item4, delegate { return defaultValue; });
        }
        public static TResult EvalOr<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, Func<T1, T2, T3, T4, TResult> otherwiseFunc)
        {
            if (func == null)
                if (otherwiseFunc == null)
                    return default(TResult);
                else
                    return otherwiseFunc(item1, item2, item3, item4); 
            return func(item1, item2, item3, item4);
        }

        public static TResult EvalOr<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, TResult defaultValue = default(TResult))
        {
            return func.EvalOr(item1, item2, item3, item4, item5, delegate { return defaultValue; });
        }
        public static TResult EvalOr<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, Func<T1, T2, T3, T4, T5, TResult> otherwiseFunc)
        {
            if (func == null)
                if (otherwiseFunc == null)
                    return default(TResult);
                else
                    return otherwiseFunc(item1, item2, item3, item4, item5); 
            return func(item1, item2, item3, item4, item5);
        }

        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, TResult defaultValue = default(TResult))
        {
            return func.EvalOr(item1, item2, item3, item4, item5, item6, delegate { return defaultValue; });
        }
        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, Func<T1, T2, T3, T4, T5, T6, TResult> otherwiseFunc)
        {
            if (func == null)
                if (otherwiseFunc == null)
                    return default(TResult);
                else
                    return otherwiseFunc(item1, item2, item3, item4, item5, item6); 
            return func(item1, item2, item3, item4, item5, item6);
        }

        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TResult defaultValue = default(TResult))
        {
            return func.EvalOr(item1, item2, item3, item4, item5, item6, item7, delegate { return defaultValue; });
        }
        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, Func<T1, T2, T3, T4, T5, T6, T7, TResult> otherwiseFunc)
        {
            if (func == null)
                if (otherwiseFunc == null)
                    return default(TResult);
                else
                    return otherwiseFunc(item1, item2, item3, item4, item5, item6, item7); 
            return func(item1, item2, item3, item4, item5, item6, item7);
        }

        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, TResult defaultValue = default(TResult))
        {
            return func.EvalOr(item1, item2, item3, item4, item5, item6, item7, item8, delegate { return defaultValue; });
        }
        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> otherwiseFunc)
        {
            if (func == null)
                if (otherwiseFunc == null)
                    return default(TResult);
                else
                    return otherwiseFunc(item1, item2, item3, item4, item5, item6, item7, item8); 
            return func(item1, item2, item3, item4, item5, item6, item7, item8);
        }

        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, TResult defaultValue = default(TResult))
        {
            return func.EvalOr(item1, item2, item3, item4, item5, item6, item7, item8, item9, delegate { return defaultValue; });
        }
        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> otherwiseFunc)
        {
            if (func == null)
                if (otherwiseFunc == null)
                    return default(TResult);
                else
                    return otherwiseFunc(item1, item2, item3, item4, item5, item6, item7, item8, item9); 
            return func(item1, item2, item3, item4, item5, item6, item7, item8, item9);
        }

        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, TResult defaultValue = default(TResult))
        {
            return func.EvalOr(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, delegate { return defaultValue; });
        }
        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> otherwiseFunc)
        {
            if (func == null)
                if (otherwiseFunc == null)
                    return default(TResult);
                else
                    return otherwiseFunc(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10); 
            return func(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10);
        }

        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, TResult defaultValue = default(TResult))
        {
            return func.EvalOr(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, delegate { return defaultValue; });
        }
        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> otherwiseFunc)
        {
            if (func == null)
                if (otherwiseFunc == null)
                    return default(TResult);
                else
                    return otherwiseFunc(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11); 
            return func(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11);
        }

        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, TResult defaultValue = default(TResult))
        {
            return func.EvalOr(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, delegate { return defaultValue; });
        }
        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> otherwiseFunc)
        {
            if (func == null)
                if (otherwiseFunc == null)
                    return default(TResult);
                else
                    return otherwiseFunc(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12); 
            return func(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12);
        }

        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, TResult defaultValue = default(TResult))
        {
            return func.EvalOr(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, item13, delegate { return defaultValue; });
        }
        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> otherwiseFunc)
        {
            if (func == null)
                if (otherwiseFunc == null)
                    return default(TResult);
                else
                    return otherwiseFunc(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, item13); 
            return func(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, item13);
        }

        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, TResult defaultValue = default(TResult))
        {
            return func.EvalOr(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, item13, item14, delegate { return defaultValue; });
        }
        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> otherwiseFunc)
        {
            if (func == null)
                if (otherwiseFunc == null)
                    return default(TResult);
                else
                    return otherwiseFunc(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, item13, item14); 
            return func(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, item13, item14);
        }

        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, TResult defaultValue = default(TResult))
        {
            return func.EvalOr(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, item13, item14, item15, delegate { return defaultValue; });
        }
        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> otherwiseFunc)
        {
            if (func == null)
                if (otherwiseFunc == null)
                    return default(TResult);
                else
                    return otherwiseFunc(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, item13, item14, item15); 
            return func(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, item13, item14, item15);
        }

        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, TResult defaultValue = default(TResult))
        {
            return func.EvalOr(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, item13, item14, item15, item16, delegate { return defaultValue; });
        }
        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> otherwiseFunc)
        {
            if (func == null)
                if (otherwiseFunc == null)
                    return default(TResult);
                else
                    return otherwiseFunc(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, item13, item14, item15, item16); 
            return func(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, item13, item14, item15, item16);
        }

        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, T17 item17, TResult defaultValue = default(TResult))
        {
            return func.EvalOr(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, item13, item14, item15, item16, item17, delegate { return defaultValue; });
        }
        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, T17 item17, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, TResult> otherwiseFunc)
        {
            if (func == null)
                if (otherwiseFunc == null)
                    return default(TResult);
                else
                    return otherwiseFunc(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, item13, item14, item15, item16, item17); 
            return func(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, item13, item14, item15, item16, item17);
        }

        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, T17 item17, T18 item18, TResult defaultValue = default(TResult))
        {
            return func.EvalOr(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, item13, item14, item15, item16, item17, item18, delegate { return defaultValue; });
        }
        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, T17 item17, T18 item18, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, TResult> otherwiseFunc)
        {
            if (func == null)
                if (otherwiseFunc == null)
                    return default(TResult);
                else
                    return otherwiseFunc(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, item13, item14, item15, item16, item17, item18); 
            return func(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, item13, item14, item15, item16, item17, item18);
        }

        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, T17 item17, T18 item18, T19 item19, TResult defaultValue = default(TResult))
        {
            return func.EvalOr(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, item13, item14, item15, item16, item17, item18, item19, delegate { return defaultValue; });
        }
        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, T17 item17, T18 item18, T19 item19, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, TResult> otherwiseFunc)
        {
            if (func == null)
                if (otherwiseFunc == null)
                    return default(TResult);
                else
                    return otherwiseFunc(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, item13, item14, item15, item16, item17, item18, item19); 
            return func(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, item13, item14, item15, item16, item17, item18, item19);
        }

        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, T17 item17, T18 item18, T19 item19, T20 item20, TResult defaultValue = default(TResult))
        {
            return func.EvalOr(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, item13, item14, item15, item16, item17, item18, item19, item20, delegate { return defaultValue; });
        }
        public static TResult EvalOr<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, TResult> func, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, T17 item17, T18 item18, T19 item19, T20 item20, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, TResult> otherwiseFunc)
        {
            if (func == null)
                if (otherwiseFunc == null)
                    return default(TResult);
                else
                    return otherwiseFunc(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, item13, item14, item15, item16, item17, item18, item19, item20); 
            return func(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, item13, item14, item15, item16, item17, item18, item19, item20);
        }
    }
}

