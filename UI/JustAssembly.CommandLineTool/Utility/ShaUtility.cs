using System;
using System.Security.Cryptography;
using System.Text;

namespace JustAssembly.CommandLineTool.Utility
{
  public class ShaUtility : IDisposable
  {
    private readonly SHA256 _sha256;
    private readonly StringBuilder _stringBuilder = new StringBuilder (64);

    public ShaUtility (SHA256 sha256)
    {
      this._sha256 = sha256;
    }

    public string ComputeHashAsString (string source)
    {
      var data = Encoding.UTF8.GetBytes (source);
      var hash = _sha256.ComputeHash (data);

      _stringBuilder.Length = 0;
      foreach (var b in hash)
        _stringBuilder.Append (b.ToString ("X2"));

      return _stringBuilder.ToString();
    }

    public void Dispose ()
    {
      _sha256?.Dispose();
    }
  }
}