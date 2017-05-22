using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FileHelpers;
using Visyn.Core.Mathematics;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    public class Vector3DConverter : ConverterBase
    {
        private readonly char delimiter = ',';

        public override object StringToField(string from)
        {
            var split = from.Split(new[] { delimiter },StringSplitOptions.RemoveEmptyEntries);
            Debug.Assert(split.Length % 3 == 0);
            int length = split.Length / 3;
            return length == 1 ? CreateVector(split) : CreateVectors(length, split);
        }

        public override string FieldToString(object fieldValue)
        {
            if (fieldValue is Vector3D) return FieldToString((Vector3D)fieldValue);
            var vectors = fieldValue as Vector3D[];
            if (vectors != null)
            {
                return string.Join(delimiter.ToString(), vectors.Select(FieldToString));
            }
            var list = fieldValue as IList<Vector3D>;
            if (list != null)
            {
                return string.Join(delimiter.ToString(), list.Select(FieldToString));
            }
            Debug.Assert(false,$"Unhandled conversion type {fieldValue.GetType()}");
            return "";
        }

        public string FieldToString(Vector3D fieldValue)
        {
            return $"{fieldValue.X}{delimiter}{fieldValue.Y}{delimiter}{fieldValue.Z}{delimiter}";
        }


        private static object CreateVectors(int length, string[] split)
        {
            var vectors = new Vector3D[length];
            for (int i = 0; i < split.Length; i += 3)
            {
                vectors[i / 3] = CreateVector(split, i);
            }
            return vectors;
        }

        private static Vector3D CreateVector(string[] split, int offset = 0)
        {
            return new Vector3D(new double[]
                { double.Parse(split[offset]), double.Parse(split[offset + 1]), double.Parse(split[offset + 2])});
        }
    }
}