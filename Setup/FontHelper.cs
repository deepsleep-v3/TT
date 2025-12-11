using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Reflection;
using System.Text;

namespace Setup
{
    internal class FontHelper
    {
        public static void ApplyEmbeddedTTFToControl(Control ctl, string embeddedResName, float size)
        {
            if (ctl == null) throw new ArgumentNullException(nameof(ctl));
            if (string.IsNullOrWhiteSpace(embeddedResName)) throw new ArgumentException("Resource name is null or write space", nameof(embeddedResName));

            var pfc = new PrivateFontCollection();
            var asm = Assembly.GetExecutingAssembly();
            using var stream = asm.GetManifestResourceStream(embeddedResName)
                          ?? throw new FileNotFoundException("Not found font resources: " + embeddedResName);

            var buffer = new byte[stream.Length];
            stream.ReadExactly(buffer, 0, buffer.Length);

            unsafe { fixed (byte* p = buffer) pfc.AddMemoryFont((IntPtr)p, buffer.Length); }

            var family = pfc.Families[0]; // 使用字体的“家族名”创建新 Font
            ctl.Font = new Font(family, size, ctl.Font.Style, ctl.Font.Unit);
        }
    }
}
