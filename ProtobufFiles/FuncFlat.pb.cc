// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: FuncFlat.proto

#include "FuncFlat.pb.h"

#include <algorithm>

#include <google/protobuf/io/coded_stream.h>
#include <google/protobuf/extension_set.h>
#include <google/protobuf/wire_format_lite.h>
#include <google/protobuf/descriptor.h>
#include <google/protobuf/generated_message_reflection.h>
#include <google/protobuf/reflection_ops.h>
#include <google/protobuf/wire_format.h>
// @@protoc_insertion_point(includes)
#include <google/protobuf/port_def.inc>
class FlatFuncDefaultTypeInternal {
 public:
  ::PROTOBUF_NAMESPACE_ID::internal::ExplicitlyConstructed<FlatFunc> _instance;
} _FlatFunc_default_instance_;
static void InitDefaultsscc_info_FlatFunc_FuncFlat_2eproto() {
  GOOGLE_PROTOBUF_VERIFY_VERSION;

  {
    void* ptr = &::_FlatFunc_default_instance_;
    new (ptr) ::FlatFunc();
    ::PROTOBUF_NAMESPACE_ID::internal::OnShutdownDestroyMessage(ptr);
  }
  ::FlatFunc::InitAsDefaultInstance();
}

::PROTOBUF_NAMESPACE_ID::internal::SCCInfo<0> scc_info_FlatFunc_FuncFlat_2eproto =
    {{ATOMIC_VAR_INIT(::PROTOBUF_NAMESPACE_ID::internal::SCCInfoBase::kUninitialized), 0, 0, InitDefaultsscc_info_FlatFunc_FuncFlat_2eproto}, {}};

static ::PROTOBUF_NAMESPACE_ID::Metadata file_level_metadata_FuncFlat_2eproto[1];
static constexpr ::PROTOBUF_NAMESPACE_ID::EnumDescriptor const** file_level_enum_descriptors_FuncFlat_2eproto = nullptr;
static constexpr ::PROTOBUF_NAMESPACE_ID::ServiceDescriptor const** file_level_service_descriptors_FuncFlat_2eproto = nullptr;

const ::PROTOBUF_NAMESPACE_ID::uint32 TableStruct_FuncFlat_2eproto::offsets[] PROTOBUF_SECTION_VARIABLE(protodesc_cold) = {
  ~0u,  // no _has_bits_
  PROTOBUF_FIELD_OFFSET(::FlatFunc, _internal_metadata_),
  ~0u,  // no _extensions_
  ~0u,  // no _oneof_case_
  ~0u,  // no _weak_field_map_
  PROTOBUF_FIELD_OFFSET(::FlatFunc, functionname_),
  PROTOBUF_FIELD_OFFSET(::FlatFunc, dllname_),
  PROTOBUF_FIELD_OFFSET(::FlatFunc, dllpath_),
};
static const ::PROTOBUF_NAMESPACE_ID::internal::MigrationSchema schemas[] PROTOBUF_SECTION_VARIABLE(protodesc_cold) = {
  { 0, -1, sizeof(::FlatFunc)},
};

static ::PROTOBUF_NAMESPACE_ID::Message const * const file_default_instances[] = {
  reinterpret_cast<const ::PROTOBUF_NAMESPACE_ID::Message*>(&::_FlatFunc_default_instance_),
};

const char descriptor_table_protodef_FuncFlat_2eproto[] PROTOBUF_SECTION_VARIABLE(protodesc_cold) =
  "\n\016FuncFlat.proto\"B\n\010FlatFunc\022\024\n\014function"
  "Name\030\001 \001(\t\022\017\n\007dllName\030\002 \001(\t\022\017\n\007dllPath\030\003"
  " \001(\tb\006proto3"
  ;
static const ::PROTOBUF_NAMESPACE_ID::internal::DescriptorTable*const descriptor_table_FuncFlat_2eproto_deps[1] = {
};
static ::PROTOBUF_NAMESPACE_ID::internal::SCCInfoBase*const descriptor_table_FuncFlat_2eproto_sccs[1] = {
  &scc_info_FlatFunc_FuncFlat_2eproto.base,
};
static ::PROTOBUF_NAMESPACE_ID::internal::once_flag descriptor_table_FuncFlat_2eproto_once;
const ::PROTOBUF_NAMESPACE_ID::internal::DescriptorTable descriptor_table_FuncFlat_2eproto = {
  false, false, descriptor_table_protodef_FuncFlat_2eproto, "FuncFlat.proto", 92,
  &descriptor_table_FuncFlat_2eproto_once, descriptor_table_FuncFlat_2eproto_sccs, descriptor_table_FuncFlat_2eproto_deps, 1, 0,
  schemas, file_default_instances, TableStruct_FuncFlat_2eproto::offsets,
  file_level_metadata_FuncFlat_2eproto, 1, file_level_enum_descriptors_FuncFlat_2eproto, file_level_service_descriptors_FuncFlat_2eproto,
};

// Force running AddDescriptors() at dynamic initialization time.
static bool dynamic_init_dummy_FuncFlat_2eproto = (static_cast<void>(::PROTOBUF_NAMESPACE_ID::internal::AddDescriptors(&descriptor_table_FuncFlat_2eproto)), true);

// ===================================================================

void FlatFunc::InitAsDefaultInstance() {
}
class FlatFunc::_Internal {
 public:
};

FlatFunc::FlatFunc(::PROTOBUF_NAMESPACE_ID::Arena* arena)
  : ::PROTOBUF_NAMESPACE_ID::Message(arena) {
  SharedCtor();
  RegisterArenaDtor(arena);
  // @@protoc_insertion_point(arena_constructor:FlatFunc)
}
FlatFunc::FlatFunc(const FlatFunc& from)
  : ::PROTOBUF_NAMESPACE_ID::Message() {
  _internal_metadata_.MergeFrom<::PROTOBUF_NAMESPACE_ID::UnknownFieldSet>(from._internal_metadata_);
  functionname_.UnsafeSetDefault(&::PROTOBUF_NAMESPACE_ID::internal::GetEmptyStringAlreadyInited());
  if (!from._internal_functionname().empty()) {
    functionname_.Set(&::PROTOBUF_NAMESPACE_ID::internal::GetEmptyStringAlreadyInited(), from._internal_functionname(),
      GetArena());
  }
  dllname_.UnsafeSetDefault(&::PROTOBUF_NAMESPACE_ID::internal::GetEmptyStringAlreadyInited());
  if (!from._internal_dllname().empty()) {
    dllname_.Set(&::PROTOBUF_NAMESPACE_ID::internal::GetEmptyStringAlreadyInited(), from._internal_dllname(),
      GetArena());
  }
  dllpath_.UnsafeSetDefault(&::PROTOBUF_NAMESPACE_ID::internal::GetEmptyStringAlreadyInited());
  if (!from._internal_dllpath().empty()) {
    dllpath_.Set(&::PROTOBUF_NAMESPACE_ID::internal::GetEmptyStringAlreadyInited(), from._internal_dllpath(),
      GetArena());
  }
  // @@protoc_insertion_point(copy_constructor:FlatFunc)
}

void FlatFunc::SharedCtor() {
  ::PROTOBUF_NAMESPACE_ID::internal::InitSCC(&scc_info_FlatFunc_FuncFlat_2eproto.base);
  functionname_.UnsafeSetDefault(&::PROTOBUF_NAMESPACE_ID::internal::GetEmptyStringAlreadyInited());
  dllname_.UnsafeSetDefault(&::PROTOBUF_NAMESPACE_ID::internal::GetEmptyStringAlreadyInited());
  dllpath_.UnsafeSetDefault(&::PROTOBUF_NAMESPACE_ID::internal::GetEmptyStringAlreadyInited());
}

FlatFunc::~FlatFunc() {
  // @@protoc_insertion_point(destructor:FlatFunc)
  SharedDtor();
  _internal_metadata_.Delete<::PROTOBUF_NAMESPACE_ID::UnknownFieldSet>();
}

void FlatFunc::SharedDtor() {
  GOOGLE_DCHECK(GetArena() == nullptr);
  functionname_.DestroyNoArena(&::PROTOBUF_NAMESPACE_ID::internal::GetEmptyStringAlreadyInited());
  dllname_.DestroyNoArena(&::PROTOBUF_NAMESPACE_ID::internal::GetEmptyStringAlreadyInited());
  dllpath_.DestroyNoArena(&::PROTOBUF_NAMESPACE_ID::internal::GetEmptyStringAlreadyInited());
}

void FlatFunc::ArenaDtor(void* object) {
  FlatFunc* _this = reinterpret_cast< FlatFunc* >(object);
  (void)_this;
}
void FlatFunc::RegisterArenaDtor(::PROTOBUF_NAMESPACE_ID::Arena*) {
}
void FlatFunc::SetCachedSize(int size) const {
  _cached_size_.Set(size);
}
const FlatFunc& FlatFunc::default_instance() {
  ::PROTOBUF_NAMESPACE_ID::internal::InitSCC(&::scc_info_FlatFunc_FuncFlat_2eproto.base);
  return *internal_default_instance();
}


void FlatFunc::Clear() {
// @@protoc_insertion_point(message_clear_start:FlatFunc)
  ::PROTOBUF_NAMESPACE_ID::uint32 cached_has_bits = 0;
  // Prevent compiler warnings about cached_has_bits being unused
  (void) cached_has_bits;

  functionname_.ClearToEmpty(&::PROTOBUF_NAMESPACE_ID::internal::GetEmptyStringAlreadyInited(), GetArena());
  dllname_.ClearToEmpty(&::PROTOBUF_NAMESPACE_ID::internal::GetEmptyStringAlreadyInited(), GetArena());
  dllpath_.ClearToEmpty(&::PROTOBUF_NAMESPACE_ID::internal::GetEmptyStringAlreadyInited(), GetArena());
  _internal_metadata_.Clear<::PROTOBUF_NAMESPACE_ID::UnknownFieldSet>();
}

const char* FlatFunc::_InternalParse(const char* ptr, ::PROTOBUF_NAMESPACE_ID::internal::ParseContext* ctx) {
#define CHK_(x) if (PROTOBUF_PREDICT_FALSE(!(x))) goto failure
  ::PROTOBUF_NAMESPACE_ID::Arena* arena = GetArena(); (void)arena;
  while (!ctx->Done(&ptr)) {
    ::PROTOBUF_NAMESPACE_ID::uint32 tag;
    ptr = ::PROTOBUF_NAMESPACE_ID::internal::ReadTag(ptr, &tag);
    CHK_(ptr);
    switch (tag >> 3) {
      // string functionName = 1;
      case 1:
        if (PROTOBUF_PREDICT_TRUE(static_cast<::PROTOBUF_NAMESPACE_ID::uint8>(tag) == 10)) {
          auto str = _internal_mutable_functionname();
          ptr = ::PROTOBUF_NAMESPACE_ID::internal::InlineGreedyStringParser(str, ptr, ctx);
          CHK_(::PROTOBUF_NAMESPACE_ID::internal::VerifyUTF8(str, "FlatFunc.functionName"));
          CHK_(ptr);
        } else goto handle_unusual;
        continue;
      // string dllName = 2;
      case 2:
        if (PROTOBUF_PREDICT_TRUE(static_cast<::PROTOBUF_NAMESPACE_ID::uint8>(tag) == 18)) {
          auto str = _internal_mutable_dllname();
          ptr = ::PROTOBUF_NAMESPACE_ID::internal::InlineGreedyStringParser(str, ptr, ctx);
          CHK_(::PROTOBUF_NAMESPACE_ID::internal::VerifyUTF8(str, "FlatFunc.dllName"));
          CHK_(ptr);
        } else goto handle_unusual;
        continue;
      // string dllPath = 3;
      case 3:
        if (PROTOBUF_PREDICT_TRUE(static_cast<::PROTOBUF_NAMESPACE_ID::uint8>(tag) == 26)) {
          auto str = _internal_mutable_dllpath();
          ptr = ::PROTOBUF_NAMESPACE_ID::internal::InlineGreedyStringParser(str, ptr, ctx);
          CHK_(::PROTOBUF_NAMESPACE_ID::internal::VerifyUTF8(str, "FlatFunc.dllPath"));
          CHK_(ptr);
        } else goto handle_unusual;
        continue;
      default: {
      handle_unusual:
        if ((tag & 7) == 4 || tag == 0) {
          ctx->SetLastTag(tag);
          goto success;
        }
        ptr = UnknownFieldParse(tag,
            _internal_metadata_.mutable_unknown_fields<::PROTOBUF_NAMESPACE_ID::UnknownFieldSet>(),
            ptr, ctx);
        CHK_(ptr != nullptr);
        continue;
      }
    }  // switch
  }  // while
success:
  return ptr;
failure:
  ptr = nullptr;
  goto success;
#undef CHK_
}

::PROTOBUF_NAMESPACE_ID::uint8* FlatFunc::_InternalSerialize(
    ::PROTOBUF_NAMESPACE_ID::uint8* target, ::PROTOBUF_NAMESPACE_ID::io::EpsCopyOutputStream* stream) const {
  // @@protoc_insertion_point(serialize_to_array_start:FlatFunc)
  ::PROTOBUF_NAMESPACE_ID::uint32 cached_has_bits = 0;
  (void) cached_has_bits;

  // string functionName = 1;
  if (this->functionname().size() > 0) {
    ::PROTOBUF_NAMESPACE_ID::internal::WireFormatLite::VerifyUtf8String(
      this->_internal_functionname().data(), static_cast<int>(this->_internal_functionname().length()),
      ::PROTOBUF_NAMESPACE_ID::internal::WireFormatLite::SERIALIZE,
      "FlatFunc.functionName");
    target = stream->WriteStringMaybeAliased(
        1, this->_internal_functionname(), target);
  }

  // string dllName = 2;
  if (this->dllname().size() > 0) {
    ::PROTOBUF_NAMESPACE_ID::internal::WireFormatLite::VerifyUtf8String(
      this->_internal_dllname().data(), static_cast<int>(this->_internal_dllname().length()),
      ::PROTOBUF_NAMESPACE_ID::internal::WireFormatLite::SERIALIZE,
      "FlatFunc.dllName");
    target = stream->WriteStringMaybeAliased(
        2, this->_internal_dllname(), target);
  }

  // string dllPath = 3;
  if (this->dllpath().size() > 0) {
    ::PROTOBUF_NAMESPACE_ID::internal::WireFormatLite::VerifyUtf8String(
      this->_internal_dllpath().data(), static_cast<int>(this->_internal_dllpath().length()),
      ::PROTOBUF_NAMESPACE_ID::internal::WireFormatLite::SERIALIZE,
      "FlatFunc.dllPath");
    target = stream->WriteStringMaybeAliased(
        3, this->_internal_dllpath(), target);
  }

  if (PROTOBUF_PREDICT_FALSE(_internal_metadata_.have_unknown_fields())) {
    target = ::PROTOBUF_NAMESPACE_ID::internal::WireFormat::InternalSerializeUnknownFieldsToArray(
        _internal_metadata_.unknown_fields<::PROTOBUF_NAMESPACE_ID::UnknownFieldSet>(::PROTOBUF_NAMESPACE_ID::UnknownFieldSet::default_instance), target, stream);
  }
  // @@protoc_insertion_point(serialize_to_array_end:FlatFunc)
  return target;
}

size_t FlatFunc::ByteSizeLong() const {
// @@protoc_insertion_point(message_byte_size_start:FlatFunc)
  size_t total_size = 0;

  ::PROTOBUF_NAMESPACE_ID::uint32 cached_has_bits = 0;
  // Prevent compiler warnings about cached_has_bits being unused
  (void) cached_has_bits;

  // string functionName = 1;
  if (this->functionname().size() > 0) {
    total_size += 1 +
      ::PROTOBUF_NAMESPACE_ID::internal::WireFormatLite::StringSize(
        this->_internal_functionname());
  }

  // string dllName = 2;
  if (this->dllname().size() > 0) {
    total_size += 1 +
      ::PROTOBUF_NAMESPACE_ID::internal::WireFormatLite::StringSize(
        this->_internal_dllname());
  }

  // string dllPath = 3;
  if (this->dllpath().size() > 0) {
    total_size += 1 +
      ::PROTOBUF_NAMESPACE_ID::internal::WireFormatLite::StringSize(
        this->_internal_dllpath());
  }

  if (PROTOBUF_PREDICT_FALSE(_internal_metadata_.have_unknown_fields())) {
    return ::PROTOBUF_NAMESPACE_ID::internal::ComputeUnknownFieldsSize(
        _internal_metadata_, total_size, &_cached_size_);
  }
  int cached_size = ::PROTOBUF_NAMESPACE_ID::internal::ToCachedSize(total_size);
  SetCachedSize(cached_size);
  return total_size;
}

void FlatFunc::MergeFrom(const ::PROTOBUF_NAMESPACE_ID::Message& from) {
// @@protoc_insertion_point(generalized_merge_from_start:FlatFunc)
  GOOGLE_DCHECK_NE(&from, this);
  const FlatFunc* source =
      ::PROTOBUF_NAMESPACE_ID::DynamicCastToGenerated<FlatFunc>(
          &from);
  if (source == nullptr) {
  // @@protoc_insertion_point(generalized_merge_from_cast_fail:FlatFunc)
    ::PROTOBUF_NAMESPACE_ID::internal::ReflectionOps::Merge(from, this);
  } else {
  // @@protoc_insertion_point(generalized_merge_from_cast_success:FlatFunc)
    MergeFrom(*source);
  }
}

void FlatFunc::MergeFrom(const FlatFunc& from) {
// @@protoc_insertion_point(class_specific_merge_from_start:FlatFunc)
  GOOGLE_DCHECK_NE(&from, this);
  _internal_metadata_.MergeFrom<::PROTOBUF_NAMESPACE_ID::UnknownFieldSet>(from._internal_metadata_);
  ::PROTOBUF_NAMESPACE_ID::uint32 cached_has_bits = 0;
  (void) cached_has_bits;

  if (from.functionname().size() > 0) {
    _internal_set_functionname(from._internal_functionname());
  }
  if (from.dllname().size() > 0) {
    _internal_set_dllname(from._internal_dllname());
  }
  if (from.dllpath().size() > 0) {
    _internal_set_dllpath(from._internal_dllpath());
  }
}

void FlatFunc::CopyFrom(const ::PROTOBUF_NAMESPACE_ID::Message& from) {
// @@protoc_insertion_point(generalized_copy_from_start:FlatFunc)
  if (&from == this) return;
  Clear();
  MergeFrom(from);
}

void FlatFunc::CopyFrom(const FlatFunc& from) {
// @@protoc_insertion_point(class_specific_copy_from_start:FlatFunc)
  if (&from == this) return;
  Clear();
  MergeFrom(from);
}

bool FlatFunc::IsInitialized() const {
  return true;
}

void FlatFunc::InternalSwap(FlatFunc* other) {
  using std::swap;
  _internal_metadata_.Swap<::PROTOBUF_NAMESPACE_ID::UnknownFieldSet>(&other->_internal_metadata_);
  functionname_.Swap(&other->functionname_, &::PROTOBUF_NAMESPACE_ID::internal::GetEmptyStringAlreadyInited(), GetArena());
  dllname_.Swap(&other->dllname_, &::PROTOBUF_NAMESPACE_ID::internal::GetEmptyStringAlreadyInited(), GetArena());
  dllpath_.Swap(&other->dllpath_, &::PROTOBUF_NAMESPACE_ID::internal::GetEmptyStringAlreadyInited(), GetArena());
}

::PROTOBUF_NAMESPACE_ID::Metadata FlatFunc::GetMetadata() const {
  return GetMetadataStatic();
}


// @@protoc_insertion_point(namespace_scope)
PROTOBUF_NAMESPACE_OPEN
template<> PROTOBUF_NOINLINE ::FlatFunc* Arena::CreateMaybeMessage< ::FlatFunc >(Arena* arena) {
  return Arena::CreateMessageInternal< ::FlatFunc >(arena);
}
PROTOBUF_NAMESPACE_CLOSE

// @@protoc_insertion_point(global_scope)
#include <google/protobuf/port_undef.inc>