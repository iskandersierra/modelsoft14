using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework
{
  public static class BinaryConvert
  {
    private static readonly Encoding CharEncoding = Encoding.UTF8;
    private static readonly Decoder CharDecoder = CharEncoding.GetDecoder();

    #region [ Boolean ]
    public const int BinaryBooleanSize = 1;

    public static int ToBinarySize(Boolean value)
    {
      return BinaryBooleanSize;
    }
    public static int ToBooleanSize(byte[] array, int startAt = 0)
    {
      return BinaryBooleanSize;
    }

    public static byte[] ToBinary(Boolean value)
    {
      var array = new byte[BinaryBooleanSize];
      ToBinaryInternal(value, array, 0);
      return array;
    }
    public static int ToBinary(Boolean value, byte[] array, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryBooleanSize, "startAt");

      return ToBinaryInternal(value, array, startAt);
    }

    public static Boolean ToBoolean(byte[] array, int startAt = 0)
    {
      Boolean value;
      ToBoolean(array, out value, startAt);
      return value;
    }
    public static int ToBoolean(byte[] array, out Boolean value, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryBooleanSize, "startAt");

      return ToBooleanInternal(array, startAt, out value);
    }

    private static int ToBinaryInternal(Boolean value, byte[] array, int startAt)
    {
      array[startAt] = (byte)(value ? 1 : 0);
      return BinaryBooleanSize;
    }
    private static int ToBooleanInternal(byte[] array, int startAt, out Boolean value)
    {
      value = array[startAt] != (byte)0;
      return BinaryBooleanSize;
    }
    #endregion

    #region [ Byte ]
    public const int BinaryByteSize = 1;

    public static int ToBinarySize(Byte value)
    {
      return BinaryByteSize;
    }
    public static int ToByteSize(byte[] array, int startAt = 0)
    {
      return BinaryByteSize;
    }

    public static byte[] ToBinary(Byte value)
    {
      var array = new byte[BinaryByteSize];
      ToBinaryInternal(value, array, 0);
      return array;
    }
    public static int ToBinary(Byte value, byte[] array, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryByteSize, "startAt");

      return ToBinaryInternal(value, array, startAt);
    }

    public static Byte ToByte(byte[] array, int startAt = 0)
    {
      Byte value;
      ToByte(array, out value, startAt);
      return value;
    }
    public static int ToByte(byte[] array, out Byte value, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryByteSize, "startAt");

      return ToByteInternal(array, startAt, out value);
    }

    private static int ToBinaryInternal(Byte value, byte[] array, int startAt)
    {
      array[startAt] = value;
      return BinaryByteSize;
    }
    private static int ToByteInternal(byte[] array, int startAt, out Byte value)
    {
      value = array[startAt];
      return BinaryByteSize;
    }
    #endregion

    #region [ SByte ]
    public const int BinarySByteSize = 1;

    public static int ToBinarySize(SByte value)
    {
      return BinarySByteSize;
    }
    public static int ToSByteSize(byte[] array, int startAt = 0)
    {
      return BinarySByteSize;
    }

    public static byte[] ToBinary(SByte value)
    {
      var array = new byte[BinarySByteSize];
      ToBinaryInternal(value, array, 0);
      return array;
    }
    public static int ToBinary(SByte value, byte[] array, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinarySByteSize, "startAt");

      return ToBinaryInternal(value, array, startAt);
    }

    public static SByte ToSByte(byte[] array, int startAt = 0)
    {
      SByte value;
      ToSByte(array, out value, startAt);
      return value;
    }
    public static int ToSByte(byte[] array, out SByte value, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinarySByteSize, "startAt");

      return ToSByteInternal(array, startAt, out value);
    }

    private static int ToBinaryInternal(SByte value, byte[] array, int startAt)
    {
      array[startAt] = (Byte)value;
      return BinarySByteSize;
    }
    private static int ToSByteInternal(byte[] array, int startAt, out SByte value)
    {
      value = (SByte)array[startAt];
      return BinarySByteSize;
    }
    #endregion

    #region [ Char ]
    public static int ToBinarySize(Char value)
    {
      char[] inbuffer = new char[] { value };
      int size = CharEncoding.GetByteCount(inbuffer);
      return size;
    }
    public static int ToCharSize(byte[] array, int startAt = 0)
    {
      int bytesUsed, charsUsed;
      bool completed;
      var chars = new char[1];
      CharDecoder.Convert(array, startAt, array.Length - startAt, chars, 0, 1, true, out bytesUsed, out charsUsed, out completed);
      if (!completed || charsUsed != 1) throw new FormatException("array");
      return bytesUsed;
    }

    public static byte[] ToBinary(Char value)
    {
      var array = new byte[ToBinarySize(value)];
      ToBinaryInternal(value, array, 0);
      return array;
    }
    public static int ToBinary(Char value, byte[] array, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - ToBinarySize(value), "startAt");

      return ToBinaryInternal(value, array, startAt);
    }

    public static Char ToChar(byte[] array, int startAt = 0)
    {
      Char value;
      ToChar(array, out value, startAt);
      return value;
    }
    public static int ToChar(byte[] array, out Char value, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - ToCharSize(array), "startAt");

      return ToCharInternal(array, startAt, out value);
    }

    private static int ToBinaryInternal(Char value, byte[] array, int startAt)
    {
      Require.Condition(!char.IsSurrogate(value), "value", Resources.ArgException_SurrogateNotAllowedAsSingleChar);

      char[] inbuffer = new char[] { value };
      return CharEncoding.GetBytes(inbuffer, 0, 1, array, startAt);
    }
    private static int ToCharInternal(byte[] array, int startAt, out Char value)
    {
      int bytesUsed, charsUsed;
      bool completed;
      var chars = new char[1];
      CharDecoder.Convert(array, startAt, array.Length - startAt, chars, 0, 1, true, out bytesUsed, out charsUsed, out completed);
      if (!completed || charsUsed != 1) throw new FormatException("array");
      value = chars[0];
      return bytesUsed;
    }
    #endregion

    #region [ Double ]
    public const int BinaryDoubleSize = 8;

    public static int ToBinarySize(Double value)
    {
      return BinaryDoubleSize;
    }
    public static int ToDoubleSize(byte[] array, int startAt = 0)
    {
      return BinaryDoubleSize;
    }

    public static byte[] ToBinary(Double value)
    {
      var array = new byte[BinaryDoubleSize];
      ToBinaryInternal(value, array, 0);
      return array;
    }
    public static int ToBinary(Double value, byte[] array, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryDoubleSize, "startAt");

      return ToBinaryInternal(value, array, startAt);
    }

    public static Double ToDouble(byte[] array, int startAt = 0)
    {
      Double value;
      ToDouble(array, out value, startAt);
      return value;
    }
    public static int ToDouble(byte[] array, out Double value, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryDoubleSize, "startAt");

      return ToDoubleInternal(array, startAt, out value);
    }

    private static unsafe int ToBinaryInternal(Double value, byte[] array, int startAt)
    {
      ulong num = *(ulong*)(&value);
      array[startAt + 0] = (byte)num;
      array[startAt + 1] = (byte)(num >> 8);
      array[startAt + 2] = (byte)(num >> 16);
      array[startAt + 3] = (byte)(num >> 24);
      array[startAt + 4] = (byte)(num >> 32);
      array[startAt + 5] = (byte)(num >> 40);
      array[startAt + 6] = (byte)(num >> 48);
      array[startAt + 7] = (byte)(num >> 56);
      return BinaryDoubleSize;
    }
    private static unsafe int ToDoubleInternal(byte[] array, int startAt, out Double value)
    {
      uint loPart = (uint)(
        (int)array[startAt + 0] | 
        (int)array[startAt + 1] << 8 | 
        (int)array[startAt + 2] << 16 | 
        (int)array[startAt + 3] << 24);

      uint hiPart = (uint)(
        (int)array[startAt + 4] | 
        (int)array[startAt + 5] << 8 | 
        (int)array[startAt + 6] << 16 | 
        (int)array[startAt + 7] << 24);

      ulong longNumber = (ulong)hiPart << 32 | (ulong)loPart;
      value = *(double*)(&longNumber);

      return BinaryDoubleSize;
    }
    #endregion

    #region [ Decimal ]
    public const int BinaryDecimalSize = 16;

    public static int ToBinarySize(Decimal value)
    {
      return BinaryDecimalSize;
    }
    public static int ToDecimalSize(byte[] array, int startAt = 0)
    {
      return BinaryDecimalSize;
    }

    public static byte[] ToBinary(Decimal value)
    {
      var array = new byte[BinaryDecimalSize];
      ToBinaryInternal(value, array, 0);
      return array;
    }
    public static int ToBinary(Decimal value, byte[] array, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryDecimalSize, "startAt");

      return ToBinaryInternal(value, array, startAt);
    }

    public static Decimal ToDecimal(byte[] array, int startAt = 0)
    {
      Decimal value;
      ToDecimal(array, out value, startAt);
      return value;
    }
    public static int ToDecimal(byte[] array, out Decimal value, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryDecimalSize, "startAt");

      return ToDecimalInternal(array, startAt, out value);
    }

    private static unsafe int ToBinaryInternal(Decimal value, byte[] array, int startAt)
    {
      uint* ptr = (uint*)&value; // 4 * UInt32
      array[startAt + 0] = (byte)(*ptr);
      array[startAt + 1] = (byte)(*ptr >> 8);
      array[startAt + 2] = (byte)(*ptr >> 16);
      array[startAt + 3] = (byte)(*ptr >> 24);
      ptr++;
      array[startAt + 4] = (byte)(*ptr);
      array[startAt + 5] = (byte)(*ptr >> 8);
      array[startAt + 6] = (byte)(*ptr >> 16);
      array[startAt + 7] = (byte)(*ptr >> 24);
      ptr++;
      array[startAt + 8] = (byte)(*ptr);
      array[startAt + 9] = (byte)(*ptr >> 8);
      array[startAt + 10] = (byte)(*ptr >> 16);
      array[startAt + 11] = (byte)(*ptr >> 24);
      ptr++;
      array[startAt + 12] = (byte)(*ptr);
      array[startAt + 13] = (byte)(*ptr >> 8);
      array[startAt + 14] = (byte)(*ptr >> 16);
      array[startAt + 15] = (byte)(*ptr >> 24);
      ptr++;

      return BinaryDecimalSize;
    }
    private static unsafe int ToDecimalInternal(byte[] array, int startAt, out Decimal value)
    {
      decimal result = 0;
      uint* ptr = (uint*)&result; // 4 * Int32

      *ptr = (uint)(
        (int)array[startAt + 0] |
        (int)array[startAt + 1] << 8 |
        (int)array[startAt + 2] << 16 |
        (int)array[startAt + 3] << 24);
      ptr++;

      *ptr = (uint)(
        (int)array[startAt + 4] |
        (int)array[startAt + 5] << 8 |
        (int)array[startAt + 6] << 16 |
        (int)array[startAt + 7] << 24);
      ptr++;

      *ptr = (uint)(
        (int)array[startAt + 8] |
        (int)array[startAt + 9] << 8 |
        (int)array[startAt + 10] << 16 |
        (int)array[startAt + 11] << 24);
      ptr++;

      *ptr = (uint)(
        (int)array[startAt + 12] |
        (int)array[startAt + 13] << 8 |
        (int)array[startAt + 14] << 16 |
        (int)array[startAt + 15] << 24);
      ptr++;

      value = result;
      return BinaryDecimalSize;
    }
    #endregion

    #region [ Int16 ]
    public const int BinaryInt16Size = 2;

    public static int ToBinarySize(Int16 value)
    {
      return BinaryInt16Size;
    }
    public static int ToInt16Size(byte[] array, int startAt = 0)
    {
      return BinaryInt16Size;
    }

    public static byte[] ToBinary(Int16 value)
    {
      var array = new byte[BinaryInt16Size];
      ToBinaryInternal(value, array, 0);
      return array;
    }
    public static int ToBinary(Int16 value, byte[] array, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryInt16Size, "startAt");

      return ToBinaryInternal(value, array, startAt);
    }

    public static Int16 ToInt16(byte[] array, int startAt = 0)
    {
      Int16 value;
      ToInt16(array, out value, startAt);
      return value;
    }
    public static int ToInt16(byte[] array, out Int16 value, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryInt16Size, "startAt");

      return ToInt16Internal(array, startAt, out value);
    }

    private static int ToBinaryInternal(Int16 value, byte[] array, int startAt)
    {
      array[startAt + 0] = (byte)value;
      array[startAt + 1] = (byte)(value >> 8);
      return BinaryInt16Size;
    }
    private static int ToInt16Internal(byte[] array, int startAt, out Int16 value)
    {
      value = (short)(
        (int)array[startAt + 0] |
        (int)array[startAt + 1] << 8);
      return BinaryInt16Size;
    }
    #endregion

    #region [ UInt16 ]
    public const int BinaryUInt16Size = 2;

    public static int ToBinarySize(UInt16 value)
    {
      return BinaryUInt16Size;
    }
    public static int ToUInt16Size(byte[] array, int startAt = 0)
    {
      return BinaryUInt16Size;
    }

    public static byte[] ToBinary(UInt16 value)
    {
      var array = new byte[BinaryUInt16Size];
      ToBinaryInternal(value, array, 0);
      return array;
    }
    public static int ToBinary(UInt16 value, byte[] array, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryUInt16Size, "startAt");

      return ToBinaryInternal(value, array, startAt);
    }

    public static UInt16 ToUInt16(byte[] array, int startAt = 0)
    {
      UInt16 value;
      ToUInt16(array, out value, startAt);
      return value;
    }
    public static int ToUInt16(byte[] array, out UInt16 value, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryUInt16Size, "startAt");

      return ToUInt16Internal(array, startAt, out value);
    }

    private static int ToBinaryInternal(UInt16 value, byte[] array, int startAt)
    {
      array[startAt + 0] = (byte)value;
      array[startAt + 1] = (byte)(value >> 8);
      return BinaryUInt16Size;
    }
    private static int ToUInt16Internal(byte[] array, int startAt, out UInt16 value)
    {
      value = (ushort)(
        (int)array[startAt + 0] |
        (int)array[startAt + 1] << 8);
      return BinaryUInt16Size;
    }
    #endregion

    #region [ Int32 ]
    public const int BinaryInt32Size = 4;

    public static int ToBinarySize(Int32 value)
    {
      return BinaryInt32Size;
    }
    public static int ToInt32Size(byte[] array, int startAt = 0)
    {
      return BinaryInt32Size;
    }

    public static byte[] ToBinary(Int32 value)
    {
      var array = new byte[BinaryInt32Size];
      ToBinaryInternal(value, array, 0);
      return array;
    }
    public static int ToBinary(Int32 value, byte[] array, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryInt32Size, "startAt");

      return ToBinaryInternal(value, array, startAt);
    }

    public static Int32 ToInt32(byte[] array, int startAt = 0)
    {
      Int32 value;
      ToInt32(array, out value, startAt);
      return value;
    }
    public static int ToInt32(byte[] array, out Int32 value, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryInt32Size, "startAt");

      return ToInt32Internal(array, startAt, out value);
    }

    private static int ToBinaryInternal(Int32 value, byte[] array, int startAt)
    {
      array[startAt + 0] = (byte)value;
      array[startAt + 1] = (byte)(value >> 8);
      array[startAt + 2] = (byte)(value >> 16);
      array[startAt + 3] = (byte)(value >> 24);
      return BinaryInt32Size;
    }
    private static int ToInt32Internal(byte[] array, int startAt, out Int32 value)
    {
      value = (int)(
        (int)array[startAt + 0] |
        (int)array[startAt + 1] << 8 |
        (int)array[startAt + 2] << 16 |
        (int)array[startAt + 3] << 24);
      return BinaryInt32Size;
    }
    #endregion

    #region [ UInt32 ]
    public const int BinaryUInt32Size = 4;

    public static int ToBinarySize(UInt32 value)
    {
      return BinaryUInt32Size;
    }
    public static int ToUInt32Size(byte[] array, int startAt = 0)
    {
      return BinaryUInt32Size;
    }

    public static byte[] ToBinary(UInt32 value)
    {
      var array = new byte[BinaryUInt32Size];
      ToBinaryInternal(value, array, 0);
      return array;
    }
    public static int ToBinary(UInt32 value, byte[] array, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryUInt32Size, "startAt");

      return ToBinaryInternal(value, array, startAt);
    }

    public static UInt32 ToUInt32(byte[] array, int startAt = 0)
    {
      UInt32 value;
      ToUInt32(array, out value, startAt);
      return value;
    }
    public static int ToUInt32(byte[] array, out UInt32 value, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryUInt32Size, "startAt");

      return ToUInt32Internal(array, startAt, out value);
    }

    private static int ToBinaryInternal(UInt32 value, byte[] array, int startAt)
    {
      array[startAt + 0] = (byte)value;
      array[startAt + 1] = (byte)(value >> 8);
      array[startAt + 2] = (byte)(value >> 16);
      array[startAt + 3] = (byte)(value >> 24);
      return BinaryUInt32Size;
    }
    private static int ToUInt32Internal(byte[] array, int startAt, out UInt32 value)
    {
      value = (uint)(
        (int)array[startAt + 0] |
        (int)array[startAt + 1] << 8 |
        (int)array[startAt + 2] << 16 |
        (int)array[startAt + 3] << 24);
      return BinaryUInt32Size;
    }
    #endregion

    #region [ Int64 ]
    public const int BinaryInt64Size = 8;

    public static int ToBinarySize(Int64 value)
    {
      return BinaryInt64Size;
    }
    public static int ToInt64Size(byte[] array, int startAt = 0)
    {
      return BinaryInt64Size;
    }

    public static byte[] ToBinary(Int64 value)
    {
      var array = new byte[BinaryInt64Size];
      ToBinaryInternal(value, array, 0);
      return array;
    }
    public static int ToBinary(Int64 value, byte[] array, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryInt64Size, "startAt");

      return ToBinaryInternal(value, array, startAt);
    }

    public static Int64 ToInt64(byte[] array, int startAt = 0)
    {
      Int64 value;
      ToInt64(array, out value, startAt);
      return value;
    }
    public static int ToInt64(byte[] array, out Int64 value, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryInt64Size, "startAt");

      return ToInt64Internal(array, startAt, out value);
    }

    private static int ToBinaryInternal(Int64 value, byte[] array, int startAt)
    {
      array[startAt + 0] = (byte)value;
      array[startAt + 1] = (byte)(value >> 8);
      array[startAt + 2] = (byte)(value >> 16);
      array[startAt + 3] = (byte)(value >> 24);
      array[startAt + 4] = (byte)(value >> 32);
      array[startAt + 5] = (byte)(value >> 40);
      array[startAt + 6] = (byte)(value >> 48);
      array[startAt + 7] = (byte)(value >> 56);
      return BinaryInt64Size;
    }
    private static int ToInt64Internal(byte[] array, int startAt, out Int64 value)
    {
      uint lo = (uint)(
        (int)array[startAt + 0] |
        (int)array[startAt + 1] << 8 |
        (int)array[startAt + 2] << 16 |
        (int)array[startAt + 3] << 24);
      uint hi = (uint)(
        (int)array[startAt + 4] |
        (int)array[startAt + 5] << 8 |
        (int)array[startAt + 6] << 16 |
        (int)array[startAt + 7] << 24);
      value = (long)((ulong)hi << 32 | (ulong)lo);
      return BinaryInt64Size;
    }
    #endregion

    #region [ UInt64 ]
    public const int BinaryUInt64Size = 8;

    public static int ToBinarySize(UInt64 value)
    {
      return BinaryUInt64Size;
    }
    public static int ToUInt64Size(byte[] array, int startAt = 0)
    {
      return BinaryUInt64Size;
    }

    public static byte[] ToBinary(UInt64 value)
    {
      var array = new byte[BinaryUInt64Size];
      ToBinaryInternal(value, array, 0);
      return array;
    }
    public static int ToBinary(UInt64 value, byte[] array, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryUInt64Size, "startAt");

      return ToBinaryInternal(value, array, startAt);
    }

    public static UInt64 ToUInt64(byte[] array, int startAt = 0)
    {
      UInt64 value;
      ToUInt64(array, out value, startAt);
      return value;
    }
    public static int ToUInt64(byte[] array, out UInt64 value, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryUInt64Size, "startAt");

      return ToUInt64Internal(array, startAt, out value);
    }

    private static int ToBinaryInternal(UInt64 value, byte[] array, int startAt)
    {
      array[startAt + 0] = (byte)value;
      array[startAt + 1] = (byte)(value >> 8);
      array[startAt + 2] = (byte)(value >> 16);
      array[startAt + 3] = (byte)(value >> 24);
      array[startAt + 4] = (byte)(value >> 32);
      array[startAt + 5] = (byte)(value >> 40);
      array[startAt + 6] = (byte)(value >> 48);
      array[startAt + 7] = (byte)(value >> 56);
      return BinaryUInt64Size;
    }
    private static int ToUInt64Internal(byte[] array, int startAt, out UInt64 value)
    {
      uint lo = (uint)(
        (int)array[startAt + 0] |
        (int)array[startAt + 1] << 8 |
        (int)array[startAt + 2] << 16 |
        (int)array[startAt + 3] << 24);
      uint hi = (uint)(
        (int)array[startAt + 4] |
        (int)array[startAt + 5] << 8 |
        (int)array[startAt + 6] << 16 |
        (int)array[startAt + 7] << 24);
      value = (ulong)((ulong)hi << 32 | (ulong)lo);
      return BinaryUInt64Size;
    }
    #endregion

    #region [ Single ]
    public const int BinarySingleSize = 4;

    public static int ToBinarySize(Single value)
    {
      return BinarySingleSize;
    }
    public static int ToSingleSize(byte[] array, int startAt = 0)
    {
      return BinarySingleSize;
    }

    public static byte[] ToBinary(Single value)
    {
      var array = new byte[BinarySingleSize];
      ToBinaryInternal(value, array, 0);
      return array;
    }
    public static int ToBinary(Single value, byte[] array, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinarySingleSize, "startAt");

      return ToBinaryInternal(value, array, startAt);
    }

    public static Single ToSingle(byte[] array, int startAt = 0)
    {
      Single value;
      ToSingle(array, out value, startAt);
      return value;
    }
    public static int ToSingle(byte[] array, out Single value, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinarySingleSize, "startAt");

      return ToSingleInternal(array, startAt, out value);
    }

    private static unsafe int ToBinaryInternal(Single value, byte[] array, int startAt)
    {
      uint num = *(uint*)(&value);
      array[startAt + 0] = (byte)num;
      array[startAt + 1] = (byte)(num >> 8);
      array[startAt + 2] = (byte)(num >> 16);
      array[startAt + 3] = (byte)(num >> 24);
      return BinarySingleSize;
    }
    private static unsafe int ToSingleInternal(byte[] array, int startAt, out Single value)
    {
      uint num = (uint)(
        (int)array[startAt + 0] |
        (int)array[startAt + 1] << 8 |
        (int)array[startAt + 2] << 16 |
        (int)array[startAt + 3] << 24);
      value = *(float*)(&num);
      return BinarySingleSize;
    }
    #endregion

    #region [ String ]
    public static int ToBinarySize(String value)
    {
      var size = CharEncoding.GetByteCount(value);;
      return size;
    }
    public static int ToStringSize(byte[] array, int startAt = 0)
    {
      var size = CharEncoding.GetCharCount(array, startAt, array.Length - startAt);
      return size;
    }

    public static byte[] ToBinary(String value)
    {
      var array = new byte[ToBinarySize(value)];
      ToBinaryInternal(value, array, 0);
      return array;
    }
    public static int ToBinary(String value, byte[] array, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - ToBinarySize(value), "startAt");

      return ToBinaryInternal(value, array, startAt);
    }

    public static String ToString(byte[] array, int startAt = 0)
    {
      String value;
      ToString(array, out value, startAt);
      return value;
    }
    public static int ToString(byte[] array, out String value, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - ToStringSize(array, startAt), "startAt");

      return ToStringInternal(array, startAt, out value);
    }

    private static int ToBinaryInternal(String value, byte[] array, int startAt)
    {
      var size = CharEncoding.GetBytes(value, 0, value.Length, array, startAt);
      return size;
    }
    private static int ToStringInternal(byte[] array, int startAt, out String value)
    {
      var size = ToStringSize(array, startAt);
      var chars = new char[size];
      CharEncoding.GetChars(array, startAt, array.Length - startAt, chars, 0);
      value = new String(chars);
      return size;
    }
    #endregion

    #region [ DateTime ]
    public const int BinaryDateTimeSize = 8;

    public static int ToBinarySize(DateTime value)
    {
      return BinaryDateTimeSize;
    }
    public static int ToDateTimeSize(byte[] array, int startAt = 0)
    {
      return BinaryDateTimeSize;
    }

    public static byte[] ToBinary(DateTime value)
    {
      var array = new byte[BinaryDateTimeSize];
      ToBinaryInternal(value, array, 0);
      return array;
    }
    public static int ToBinary(DateTime value, byte[] array, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryDateTimeSize, "startAt");

      return ToBinaryInternal(value, array, startAt);
    }

    public static DateTime ToDateTime(byte[] array, int startAt = 0)
    {
      DateTime value;
      ToDateTime(array, out value, startAt);
      return value;
    }
    public static int ToDateTime(byte[] array, out DateTime value, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryDateTimeSize, "startAt");

      return ToDateTimeInternal(array, startAt, out value);
    }

    private static unsafe int ToBinaryInternal(DateTime value, byte[] array, int startAt)
    {
      uint* ptr = (uint*)&value; // 2 * UInt32
      array[startAt + 0] = (byte)(*ptr);
      array[startAt + 1] = (byte)(*ptr >> 8);
      array[startAt + 2] = (byte)(*ptr >> 16);
      array[startAt + 3] = (byte)(*ptr >> 24);
      ptr++;
      array[startAt + 4] = (byte)(*ptr);
      array[startAt + 5] = (byte)(*ptr >> 8);
      array[startAt + 6] = (byte)(*ptr >> 16);
      array[startAt + 7] = (byte)(*ptr >> 24);
      ptr++;

      return BinaryDateTimeSize;
    }
    private static unsafe int ToDateTimeInternal(byte[] array, int startAt, out DateTime value)
    {
      DateTime result;
      uint* ptr = (uint*)&result; // 2 * Int32

      *ptr = (uint)(
        (int)array[startAt + 0] |
        (int)array[startAt + 1] << 8 |
        (int)array[startAt + 2] << 16 |
        (int)array[startAt + 3] << 24);
      ptr++;

      *ptr = (uint)(
        (int)array[startAt + 4] |
        (int)array[startAt + 5] << 8 |
        (int)array[startAt + 6] << 16 |
        (int)array[startAt + 7] << 24);
      ptr++;

      value = result;
      return BinaryDateTimeSize;
    }
    #endregion

    #region [ DateTimeOffset ]
    public const int BinaryDateTimeOffsetSize = 12;

    public static int ToBinarySize(DateTimeOffset value)
    {
      return BinaryDateTimeOffsetSize;
    }
    public static int ToDateTimeOffsetSize(byte[] array, int startAt = 0)
    {
      return BinaryDateTimeOffsetSize;
    }

    public static byte[] ToBinary(DateTimeOffset value)
    {
      var array = new byte[BinaryDateTimeOffsetSize];
      ToBinaryInternal(value, array, 0);
      return array;
    }
    public static int ToBinary(DateTimeOffset value, byte[] array, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryDateTimeOffsetSize, "startAt");

      return ToBinaryInternal(value, array, startAt);
    }

    public static DateTimeOffset ToDateTimeOffset(byte[] array, int startAt = 0)
    {
      DateTimeOffset value;
      ToDateTimeOffset(array, out value, startAt);
      return value;
    }
    public static int ToDateTimeOffset(byte[] array, out DateTimeOffset value, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryDateTimeOffsetSize, "startAt");

      return ToDateTimeOffsetInternal(array, startAt, out value);
    }

    private static unsafe int ToBinaryInternal(DateTimeOffset value, byte[] array, int startAt)
    {
      byte* ptr = (byte*)&value;
      for (int i = 0; i < BinaryDateTimeOffsetSize; i++)
      {
        array[startAt + i] = *ptr;
        ptr++;
      }

      return BinaryDateTimeOffsetSize;
    }
    private static unsafe int ToDateTimeOffsetInternal(byte[] array, int startAt, out DateTimeOffset value)
    {
      DateTimeOffset result;

      byte* ptr = (byte*)&result;
      for (int i = 0; i < BinaryDateTimeOffsetSize; i++)
      {
        *ptr = array[startAt + i];
        ptr++;
      }

      value = result;
      return BinaryDateTimeOffsetSize;
    }
    #endregion

    #region [ TimeSpan ]
    public const int BinaryTimeSpanSize = 8;

    public static int ToBinarySize(TimeSpan value)
    {
      return BinaryTimeSpanSize;
    }
    public static int ToTimeSpanSize(byte[] array, int startAt = 0)
    {
      return BinaryTimeSpanSize;
    }

    public static byte[] ToBinary(TimeSpan value)
    {
      var array = new byte[BinaryTimeSpanSize];
      ToBinaryInternal(value, array, 0);
      return array;
    }
    public static int ToBinary(TimeSpan value, byte[] array, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryTimeSpanSize, "startAt");

      return ToBinaryInternal(value, array, startAt);
    }

    public static TimeSpan ToTimeSpan(byte[] array, int startAt = 0)
    {
      TimeSpan value;
      ToTimeSpan(array, out value, startAt);
      return value;
    }
    public static int ToTimeSpan(byte[] array, out TimeSpan value, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryTimeSpanSize, "startAt");

      return ToTimeSpanInternal(array, startAt, out value);
    }

    private static unsafe int ToBinaryInternal(TimeSpan value, byte[] array, int startAt)
    {
      uint* ptr = (uint*)&value; // 2 * UInt32
      array[startAt + 0] = (byte)(*ptr);
      array[startAt + 1] = (byte)(*ptr >> 8);
      array[startAt + 2] = (byte)(*ptr >> 16);
      array[startAt + 3] = (byte)(*ptr >> 24);
      ptr++;
      array[startAt + 4] = (byte)(*ptr);
      array[startAt + 5] = (byte)(*ptr >> 8);
      array[startAt + 6] = (byte)(*ptr >> 16);
      array[startAt + 7] = (byte)(*ptr >> 24);
      ptr++;

      return BinaryTimeSpanSize;
    }
    private static unsafe int ToTimeSpanInternal(byte[] array, int startAt, out TimeSpan value)
    {
      TimeSpan result;
      uint* ptr = (uint*)&result; // 2 * Int32

      *ptr = (uint)(
        (int)array[startAt + 0] |
        (int)array[startAt + 1] << 8 |
        (int)array[startAt + 2] << 16 |
        (int)array[startAt + 3] << 24);
      ptr++;

      *ptr = (uint)(
        (int)array[startAt + 4] |
        (int)array[startAt + 5] << 8 |
        (int)array[startAt + 6] << 16 |
        (int)array[startAt + 7] << 24);
      ptr++;

      value = result;
      return BinaryTimeSpanSize;
    }
    #endregion

    #region [ Guid ]
    public const int BinaryGuidSize = 16;

    public static int ToBinarySize(Guid value)
    {
      return BinaryGuidSize;
    }
    public static int ToGuidSize(byte[] array, int startAt = 0)
    {
      return BinaryGuidSize;
    }

    public static byte[] ToBinary(Guid value)
    {
      var array = new byte[BinaryGuidSize];
      ToBinaryInternal(value, array, 0);
      return array;
    }
    public static int ToBinary(Guid value, byte[] array, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryGuidSize, "startAt");

      return ToBinaryInternal(value, array, startAt);
    }

    public static Guid ToGuid(byte[] array, int startAt = 0)
    {
      Guid value;
      ToGuid(array, out value, startAt);
      return value;
    }
    public static int ToGuid(byte[] array, out Guid value, int startAt = 0)
    {
      array.RequireNotNull("array");
      startAt.RequireInRange(0, array.Length - BinaryGuidSize, "startAt");

      return ToGuidInternal(array, startAt, out value);
    }

    private static unsafe int ToBinaryInternal(Guid value, byte[] array, int startAt)
    {
      byte* ptr = (byte*)&value;
      for (int i = 0; i < BinaryGuidSize; i++)
      {
        array[startAt + i] = *ptr;
        ptr++;
      }

      return BinaryGuidSize;
    }
    private static unsafe int ToGuidInternal(byte[] array, int startAt, out Guid value)
    {
      Guid result;

      byte* ptr = (byte*)&result;
      for (int i = 0; i < BinaryGuidSize; i++)
      {
        *ptr = array[startAt + i];
        ptr++;
      }

      value = result;
      return BinaryGuidSize;
    }
    #endregion

  }
}
