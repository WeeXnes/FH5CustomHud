using System;

namespace FH5CustomHud.Core
{
    public static class Globals
    {
        public static UpdateVar<int> speed = new UpdateVar<int>();
        public static UpdateVar<int> RPM = new UpdateVar<int>();
        public static UpdateVar<int> RPM_MAX = new UpdateVar<int>();
        public static UpdateVar<double> BOOST = new UpdateVar<double>();
        public static UpdateVar<string> shutdowntrigger = new UpdateVar<string>();
        
        public static bool AutoHide = false;
        public class UpdateVar<T>
        {
            private T _value;

            public Action ValueChanged;

            public T Value
            {
                get => _value;

                set
                {
                    _value = value;
                    OnValueChanged();
                }
            }

            protected virtual void OnValueChanged() => ValueChanged?.Invoke();
        }
        public static float GetSingle(byte[] bytes, int index)
        {
            ByteCheck(bytes, index, 4);
            return BitConverter.ToSingle(bytes, index);
        }

        public static uint GetUInt16(byte[] bytes, int index)
        {
            ByteCheck(bytes, index, 2);
            return BitConverter.ToUInt16(bytes, index);
        }

        public static uint GetUInt32(byte[] bytes, int index)
        {
            ByteCheck(bytes, index, 4);
            return BitConverter.ToUInt32(bytes, index);
        }

        public static uint GetUInt8(byte[] bytes, int index)
        {
            ByteCheck(bytes, index, 1);
            return bytes[index];
        }

        public static int GetInt8(byte[] bytes, int index)
        {
            ByteCheck(bytes, index, 1);
            return Convert.ToInt16((sbyte)bytes[index]);
        }

        private static void ByteCheck(byte[] bytes, int index, int byteCount)
        {
            if (index + byteCount <= bytes.Length)
            {
                return;
            }

            throw new ArgumentException("Not enough bytes in this array");
        }
    }
}