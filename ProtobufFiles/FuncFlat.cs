// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: FuncFlat.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from FuncFlat.proto</summary>
public static partial class FuncFlatReflection {

  #region Descriptor
  /// <summary>File descriptor for FuncFlat.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static FuncFlatReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "Cg5GdW5jRmxhdC5wcm90byJCCghGbGF0RnVuYxIUCgxmdW5jdGlvbk5hbWUY",
          "ASABKAkSDwoHZGxsTmFtZRgCIAEoCRIPCgdkbGxQYXRoGAMgASgJIoUBCgpy",
          "ZXR1cm5EYXRhEhAKCHBhc3NGYWlsGAEgASgIEhEKCWV4Y2VwdGlvbhgCIAEo",
          "CRISCgpzdGFydF90aW1lGAMgASgJEhUKDWNvbXBsZXRlX3RpbWUYBCABKAkS",
          "EAoIZGxsX25hbWUYBSABKAkSFQoNZnVuY3Rpb25fbmFtZRgGIAEoCWIGcHJv",
          "dG8z"));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::FlatFunc), global::FlatFunc.Parser, new[]{ "FunctionName", "DllName", "DllPath" }, null, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::returnData), global::returnData.Parser, new[]{ "PassFail", "Exception", "StartTime", "CompleteTime", "DllName", "FunctionName" }, null, null, null, null)
        }));
  }
  #endregion

}
#region Messages
public sealed partial class FlatFunc : pb::IMessage<FlatFunc> {
  private static readonly pb::MessageParser<FlatFunc> _parser = new pb::MessageParser<FlatFunc>(() => new FlatFunc());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<FlatFunc> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::FuncFlatReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public FlatFunc() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public FlatFunc(FlatFunc other) : this() {
    functionName_ = other.functionName_;
    dllName_ = other.dllName_;
    dllPath_ = other.dllPath_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public FlatFunc Clone() {
    return new FlatFunc(this);
  }

  /// <summary>Field number for the "functionName" field.</summary>
  public const int FunctionNameFieldNumber = 1;
  private string functionName_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string FunctionName {
    get { return functionName_; }
    set {
      functionName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "dllName" field.</summary>
  public const int DllNameFieldNumber = 2;
  private string dllName_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string DllName {
    get { return dllName_; }
    set {
      dllName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "dllPath" field.</summary>
  public const int DllPathFieldNumber = 3;
  private string dllPath_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string DllPath {
    get { return dllPath_; }
    set {
      dllPath_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as FlatFunc);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(FlatFunc other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (FunctionName != other.FunctionName) return false;
    if (DllName != other.DllName) return false;
    if (DllPath != other.DllPath) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (FunctionName.Length != 0) hash ^= FunctionName.GetHashCode();
    if (DllName.Length != 0) hash ^= DllName.GetHashCode();
    if (DllPath.Length != 0) hash ^= DllPath.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (FunctionName.Length != 0) {
      output.WriteRawTag(10);
      output.WriteString(FunctionName);
    }
    if (DllName.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(DllName);
    }
    if (DllPath.Length != 0) {
      output.WriteRawTag(26);
      output.WriteString(DllPath);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (FunctionName.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(FunctionName);
    }
    if (DllName.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(DllName);
    }
    if (DllPath.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(DllPath);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(FlatFunc other) {
    if (other == null) {
      return;
    }
    if (other.FunctionName.Length != 0) {
      FunctionName = other.FunctionName;
    }
    if (other.DllName.Length != 0) {
      DllName = other.DllName;
    }
    if (other.DllPath.Length != 0) {
      DllPath = other.DllPath;
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 10: {
          FunctionName = input.ReadString();
          break;
        }
        case 18: {
          DllName = input.ReadString();
          break;
        }
        case 26: {
          DllPath = input.ReadString();
          break;
        }
      }
    }
  }

}

public sealed partial class returnData : pb::IMessage<returnData> {
  private static readonly pb::MessageParser<returnData> _parser = new pb::MessageParser<returnData>(() => new returnData());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<returnData> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::FuncFlatReflection.Descriptor.MessageTypes[1]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public returnData() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public returnData(returnData other) : this() {
    passFail_ = other.passFail_;
    exception_ = other.exception_;
    startTime_ = other.startTime_;
    completeTime_ = other.completeTime_;
    dllName_ = other.dllName_;
    functionName_ = other.functionName_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public returnData Clone() {
    return new returnData(this);
  }

  /// <summary>Field number for the "passFail" field.</summary>
  public const int PassFailFieldNumber = 1;
  private bool passFail_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool PassFail {
    get { return passFail_; }
    set {
      passFail_ = value;
    }
  }

  /// <summary>Field number for the "exception" field.</summary>
  public const int ExceptionFieldNumber = 2;
  private string exception_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string Exception {
    get { return exception_; }
    set {
      exception_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "start_time" field.</summary>
  public const int StartTimeFieldNumber = 3;
  private string startTime_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string StartTime {
    get { return startTime_; }
    set {
      startTime_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "complete_time" field.</summary>
  public const int CompleteTimeFieldNumber = 4;
  private string completeTime_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string CompleteTime {
    get { return completeTime_; }
    set {
      completeTime_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "dll_name" field.</summary>
  public const int DllNameFieldNumber = 5;
  private string dllName_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string DllName {
    get { return dllName_; }
    set {
      dllName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "function_name" field.</summary>
  public const int FunctionNameFieldNumber = 6;
  private string functionName_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string FunctionName {
    get { return functionName_; }
    set {
      functionName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as returnData);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(returnData other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (PassFail != other.PassFail) return false;
    if (Exception != other.Exception) return false;
    if (StartTime != other.StartTime) return false;
    if (CompleteTime != other.CompleteTime) return false;
    if (DllName != other.DllName) return false;
    if (FunctionName != other.FunctionName) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (PassFail != false) hash ^= PassFail.GetHashCode();
    if (Exception.Length != 0) hash ^= Exception.GetHashCode();
    if (StartTime.Length != 0) hash ^= StartTime.GetHashCode();
    if (CompleteTime.Length != 0) hash ^= CompleteTime.GetHashCode();
    if (DllName.Length != 0) hash ^= DllName.GetHashCode();
    if (FunctionName.Length != 0) hash ^= FunctionName.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (PassFail != false) {
      output.WriteRawTag(8);
      output.WriteBool(PassFail);
    }
    if (Exception.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(Exception);
    }
    if (StartTime.Length != 0) {
      output.WriteRawTag(26);
      output.WriteString(StartTime);
    }
    if (CompleteTime.Length != 0) {
      output.WriteRawTag(34);
      output.WriteString(CompleteTime);
    }
    if (DllName.Length != 0) {
      output.WriteRawTag(42);
      output.WriteString(DllName);
    }
    if (FunctionName.Length != 0) {
      output.WriteRawTag(50);
      output.WriteString(FunctionName);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (PassFail != false) {
      size += 1 + 1;
    }
    if (Exception.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Exception);
    }
    if (StartTime.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(StartTime);
    }
    if (CompleteTime.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(CompleteTime);
    }
    if (DllName.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(DllName);
    }
    if (FunctionName.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(FunctionName);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(returnData other) {
    if (other == null) {
      return;
    }
    if (other.PassFail != false) {
      PassFail = other.PassFail;
    }
    if (other.Exception.Length != 0) {
      Exception = other.Exception;
    }
    if (other.StartTime.Length != 0) {
      StartTime = other.StartTime;
    }
    if (other.CompleteTime.Length != 0) {
      CompleteTime = other.CompleteTime;
    }
    if (other.DllName.Length != 0) {
      DllName = other.DllName;
    }
    if (other.FunctionName.Length != 0) {
      FunctionName = other.FunctionName;
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 8: {
          PassFail = input.ReadBool();
          break;
        }
        case 18: {
          Exception = input.ReadString();
          break;
        }
        case 26: {
          StartTime = input.ReadString();
          break;
        }
        case 34: {
          CompleteTime = input.ReadString();
          break;
        }
        case 42: {
          DllName = input.ReadString();
          break;
        }
        case 50: {
          FunctionName = input.ReadString();
          break;
        }
      }
    }
  }

}

#endregion


#endregion Designer generated code
