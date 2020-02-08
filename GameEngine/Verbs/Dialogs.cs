using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace GameEngine.Verbs
{
  public static class Dialogs
  {
    [DllImport("comdlg32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    internal static extern bool GetOpenFileName(ref OpenFileName ofn);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct OpenFileName
    {
      public int lStructSize;
      public IntPtr hwndOwner;
      public IntPtr hInstance;
      public string lpstrFilter;
      public string lpstrCustomFilter;
      public int nMaxCustFilter;
      public int nFilterIndex;
      public string lpstrFile;
      public int nMaxFile;
      public string lpstrFileTitle;
      public int nMaxFileTitle;
      public string lpstrInitialDir;
      public string lpstrTitle;
      public int Flags;
      public short nFileOffset;
      public short nFileExtension;
      public string lpstrDefExt;
      public IntPtr lCustData;
      public IntPtr lpfnHook;
      public string lpTemplateName;
      public IntPtr pvReserved;
      public int dwReserved;
      public int flagsEx;
    }

    public static bool OpenFileDialog(string title, string directory, string filter, out string selectedFile)
    {
      selectedFile = string.Empty;
      var ofn = new OpenFileName();
      ofn.lStructSize = Marshal.SizeOf(ofn);
      ofn.lpstrFilter = "All files(*.*)\0\0";
      ofn.lpstrFile = new string(new char[256]);
      ofn.nMaxFile = ofn.lpstrFile.Length;
      ofn.lpstrFileTitle = new string(new char[64]);
      ofn.nMaxFileTitle = ofn.lpstrFileTitle.Length;
      ofn.lpstrTitle = "Open File Dialog...";
      bool result = GetOpenFileName(ref ofn);
      if (result)
      {
        selectedFile = ofn.lpstrFile;
      }

      return result;
    }

  }
}
