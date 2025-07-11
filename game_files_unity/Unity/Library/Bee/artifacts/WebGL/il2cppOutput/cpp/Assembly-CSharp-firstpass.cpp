#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>
#include <stdint.h>


template <typename T1, typename T2, typename T3>
struct VirtualActionInvoker3
{
	typedef void (*Action)(void*, T1, T2, T3, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1, T2 p2, T3 p3)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		((Action)invokeData.methodPtr)(obj, p1, p2, p3, invokeData.method);
	}
};
template <typename R>
struct VirtualFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename R, typename T1>
struct VirtualFuncInvoker1
{
	typedef R (*Func)(void*, T1, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
template <typename R, typename T1, typename T2>
struct VirtualFuncInvoker2
{
	typedef R (*Func)(void*, T1, T2, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1, T2 p2)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, p1, p2, invokeData.method);
	}
};

// System.Collections.Generic.List`1<System.Object>
struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D;
// System.Collections.Generic.List`1<UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey>
struct List_1_tCD3B17EC7F519BA836765E50183BF8C97D94302A;
// System.Byte[]
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
// System.Char[]
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
// System.Delegate[]
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
// System.IntPtr[]
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
// System.Object[]
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
// System.Diagnostics.StackTrace[]
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
// System.String[]
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248;
// System.Type[]
struct TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB;
// UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey[]
struct FieldWithRemoteSettingsKeyU5BU5D_tAC891C2C86C33EBA44FA8BD827E052C17A6D8D37;
// System.Reflection.Binder
struct Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235;
// System.DelegateData
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
// UnityEngine.Analytics.DriveableProperty
struct DriveableProperty_tC1D2C98D55A7EB8BDC2F9E8F0EBB78888D4AD158;
// System.Reflection.FieldInfo
struct FieldInfo_t;
// System.Collections.IDictionary
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
// System.InvalidOperationException
struct InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB;
// System.Reflection.MemberFilter
struct MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553;
// System.Reflection.MemberInfo
struct MemberInfo_t;
// System.Reflection.MethodInfo
struct MethodInfo_t;
// UnityEngine.MonoBehaviour
struct MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71;
// UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C;
// System.Reflection.ParameterInfo
struct ParameterInfo_tBC2D68304851A59EFB2EAE6B168714CD45445F2F;
// System.Reflection.PropertyInfo
struct PropertyInfo_t;
// UnityEngine.Analytics.RemoteSettings
struct RemoteSettings_tBBDE520F6CF4BC18914F1A4A058CF33ECC003F75;
// System.Runtime.Serialization.SafeSerializationManager
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
// System.String
struct String_t;
// System.Type
struct Type_t;
// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
// UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey
struct FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121;
// UnityEngine.RemoteSettings/UpdatedEventHandler
struct UpdatedEventHandler_tB0D5A5BA322FE093894992C29DCF51E7E12579C4;

IL2CPP_EXTERN_C RuntimeClass* Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* DriveableProperty_tC1D2C98D55A7EB8BDC2F9E8F0EBB78888D4AD158_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Type_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* UpdatedEventHandler_tB0D5A5BA322FE093894992C29DCF51E7E12579C4_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral0BA8CB3B900ECEF5E697192B8CDA6B626EB0CE72;
IL2CPP_EXTERN_C String_t* _stringLiteral2F1705A1AA8BA6FCE863E7F2CBA4BC28458C77AE;
IL2CPP_EXTERN_C String_t* _stringLiteral673CC9996FD90AFE21BD8D0E6E6824353AF4BDA2;
IL2CPP_EXTERN_C String_t* _stringLiteralCDE9DA404E87DC408CE8E83F2431F91A958C9ABB;
IL2CPP_EXTERN_C String_t* _stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709;
IL2CPP_EXTERN_C String_t* _stringLiteralE3DFC065B6A6D9931B797808DD066491AAB82B29;
IL2CPP_EXTERN_C String_t* _stringLiteralEAFF286356193975BDF7D1E2C8BE0356352A4BF2;
IL2CPP_EXTERN_C String_t* _stringLiteralF5710B540AE7F103C278A568381FDD4AF48934CA;
IL2CPP_EXTERN_C const RuntimeMethod* FieldWithRemoteSettingsKey_SetValueRecursive_m0E72D0080DC0B6AA5BA0C9E4861355CCF83B177A_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_get_Count_mF20CB9BE944B8F7DA3107E94690C3A7D29E3CB4A_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_get_Item_m3599DA91AE71D5F6EBE6CFFEBC26A75B63F57D91_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* RemoteSettings_RemoteSettingsUpdated_m69908D5B790A05F22B83F316806CE30D32F0E1B3_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeType* Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_0_0_0_var;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;
struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;

struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
struct ParameterInfoU5BU5D_t86995AB4A1693393FE29B058CC3FD727DF0B984C;
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// <Module>
struct U3CModuleU3E_tF062866229C4952B8051AD32AB6E9D931142CC95 
{
};

// System.Collections.Generic.List`1<System.Object>
struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D  : public RuntimeObject
{
	// T[] System.Collections.Generic.List`1::_items
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ____items_1;
	// System.Int32 System.Collections.Generic.List`1::_size
	int32_t ____size_2;
	// System.Int32 System.Collections.Generic.List`1::_version
	int32_t ____version_3;
	// System.Object System.Collections.Generic.List`1::_syncRoot
	RuntimeObject* ____syncRoot_4;
};

struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D_StaticFields
{
	// T[] System.Collections.Generic.List`1::s_emptyArray
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___s_emptyArray_5;
};

// System.Collections.Generic.List`1<UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey>
struct List_1_tCD3B17EC7F519BA836765E50183BF8C97D94302A  : public RuntimeObject
{
	// T[] System.Collections.Generic.List`1::_items
	FieldWithRemoteSettingsKeyU5BU5D_tAC891C2C86C33EBA44FA8BD827E052C17A6D8D37* ____items_1;
	// System.Int32 System.Collections.Generic.List`1::_size
	int32_t ____size_2;
	// System.Int32 System.Collections.Generic.List`1::_version
	int32_t ____version_3;
	// System.Object System.Collections.Generic.List`1::_syncRoot
	RuntimeObject* ____syncRoot_4;
};

struct List_1_tCD3B17EC7F519BA836765E50183BF8C97D94302A_StaticFields
{
	// T[] System.Collections.Generic.List`1::s_emptyArray
	FieldWithRemoteSettingsKeyU5BU5D_tAC891C2C86C33EBA44FA8BD827E052C17A6D8D37* ___s_emptyArray_5;
};
struct Il2CppArrayBounds;

// UnityEngine.Analytics.DriveableProperty
struct DriveableProperty_tC1D2C98D55A7EB8BDC2F9E8F0EBB78888D4AD158  : public RuntimeObject
{
	// System.Collections.Generic.List`1<UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey> UnityEngine.Analytics.DriveableProperty::m_Fields
	List_1_tCD3B17EC7F519BA836765E50183BF8C97D94302A* ___m_Fields_0;
};

// System.Reflection.MemberInfo
struct MemberInfo_t  : public RuntimeObject
{
};

// System.Reflection.ParameterInfo
struct ParameterInfo_tBC2D68304851A59EFB2EAE6B168714CD45445F2F  : public RuntimeObject
{
	// System.Reflection.ParameterAttributes System.Reflection.ParameterInfo::AttrsImpl
	int32_t ___AttrsImpl_0;
	// System.Type System.Reflection.ParameterInfo::ClassImpl
	Type_t* ___ClassImpl_1;
	// System.Object System.Reflection.ParameterInfo::DefaultValueImpl
	RuntimeObject* ___DefaultValueImpl_2;
	// System.Reflection.MemberInfo System.Reflection.ParameterInfo::MemberImpl
	MemberInfo_t* ___MemberImpl_3;
	// System.String System.Reflection.ParameterInfo::NameImpl
	String_t* ___NameImpl_4;
	// System.Int32 System.Reflection.ParameterInfo::PositionImpl
	int32_t ___PositionImpl_5;
};
// Native definition for P/Invoke marshalling of System.Reflection.ParameterInfo
struct ParameterInfo_tBC2D68304851A59EFB2EAE6B168714CD45445F2F_marshaled_pinvoke
{
	int32_t ___AttrsImpl_0;
	Type_t* ___ClassImpl_1;
	Il2CppIUnknown* ___DefaultValueImpl_2;
	MemberInfo_t* ___MemberImpl_3;
	char* ___NameImpl_4;
	int32_t ___PositionImpl_5;
};
// Native definition for COM marshalling of System.Reflection.ParameterInfo
struct ParameterInfo_tBC2D68304851A59EFB2EAE6B168714CD45445F2F_marshaled_com
{
	int32_t ___AttrsImpl_0;
	Type_t* ___ClassImpl_1;
	Il2CppIUnknown* ___DefaultValueImpl_2;
	MemberInfo_t* ___MemberImpl_3;
	Il2CppChar* ___NameImpl_4;
	int32_t ___PositionImpl_5;
};

// System.String
struct String_t  : public RuntimeObject
{
	// System.Int32 System.String::_stringLength
	int32_t ____stringLength_4;
	// System.Char System.String::_firstChar
	Il2CppChar ____firstChar_5;
};

struct String_t_StaticFields
{
	// System.String System.String::Empty
	String_t* ___Empty_6;
};

// System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
// Native definition for P/Invoke marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
// Native definition for COM marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};

// UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey
struct FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121  : public RuntimeObject
{
	// UnityEngine.Object UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey::m_Target
	Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___m_Target_0;
	// System.String UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey::m_FieldPath
	String_t* ___m_FieldPath_1;
	// System.String UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey::m_RSKeyName
	String_t* ___m_RSKeyName_2;
	// System.String UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey::m_Type
	String_t* ___m_Type_3;
};

// System.Boolean
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	// System.Boolean System.Boolean::m_value
	bool ___m_value_0;
};

struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	// System.String System.Boolean::TrueString
	String_t* ___TrueString_5;
	// System.String System.Boolean::FalseString
	String_t* ___FalseString_6;
};

// System.Char
struct Char_t521A6F19B456D956AF452D926C32709DC03D6B17 
{
	// System.Char System.Char::m_value
	Il2CppChar ___m_value_0;
};

struct Char_t521A6F19B456D956AF452D926C32709DC03D6B17_StaticFields
{
	// System.Byte[] System.Char::s_categoryForLatin1
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___s_categoryForLatin1_3;
};

// System.Reflection.FieldInfo
struct FieldInfo_t  : public MemberInfo_t
{
};

// System.Int32
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	// System.Int32 System.Int32::m_value
	int32_t ___m_value_0;
};

// System.IntPtr
struct IntPtr_t 
{
	// System.Void* System.IntPtr::m_value
	void* ___m_value_0;
};

struct IntPtr_t_StaticFields
{
	// System.IntPtr System.IntPtr::Zero
	intptr_t ___Zero_1;
};

// System.Reflection.PropertyInfo
struct PropertyInfo_t  : public MemberInfo_t
{
};

// System.Single
struct Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C 
{
	// System.Single System.Single::m_value
	float ___m_value_0;
};

// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};

// System.Delegate
struct Delegate_t  : public RuntimeObject
{
	// System.IntPtr System.Delegate::method_ptr
	Il2CppMethodPointer ___method_ptr_0;
	// System.IntPtr System.Delegate::invoke_impl
	intptr_t ___invoke_impl_1;
	// System.Object System.Delegate::m_target
	RuntimeObject* ___m_target_2;
	// System.IntPtr System.Delegate::method
	intptr_t ___method_3;
	// System.IntPtr System.Delegate::delegate_trampoline
	intptr_t ___delegate_trampoline_4;
	// System.IntPtr System.Delegate::extra_arg
	intptr_t ___extra_arg_5;
	// System.IntPtr System.Delegate::method_code
	intptr_t ___method_code_6;
	// System.IntPtr System.Delegate::interp_method
	intptr_t ___interp_method_7;
	// System.IntPtr System.Delegate::interp_invoke_impl
	intptr_t ___interp_invoke_impl_8;
	// System.Reflection.MethodInfo System.Delegate::method_info
	MethodInfo_t* ___method_info_9;
	// System.Reflection.MethodInfo System.Delegate::original_method_info
	MethodInfo_t* ___original_method_info_10;
	// System.DelegateData System.Delegate::data
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	// System.Boolean System.Delegate::method_is_virtual
	bool ___method_is_virtual_12;
};
// Native definition for P/Invoke marshalling of System.Delegate
struct Delegate_t_marshaled_pinvoke
{
	intptr_t ___method_ptr_0;
	intptr_t ___invoke_impl_1;
	Il2CppIUnknown* ___m_target_2;
	intptr_t ___method_3;
	intptr_t ___delegate_trampoline_4;
	intptr_t ___extra_arg_5;
	intptr_t ___method_code_6;
	intptr_t ___interp_method_7;
	intptr_t ___interp_invoke_impl_8;
	MethodInfo_t* ___method_info_9;
	MethodInfo_t* ___original_method_info_10;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	int32_t ___method_is_virtual_12;
};
// Native definition for COM marshalling of System.Delegate
struct Delegate_t_marshaled_com
{
	intptr_t ___method_ptr_0;
	intptr_t ___invoke_impl_1;
	Il2CppIUnknown* ___m_target_2;
	intptr_t ___method_3;
	intptr_t ___delegate_trampoline_4;
	intptr_t ___extra_arg_5;
	intptr_t ___method_code_6;
	intptr_t ___interp_method_7;
	intptr_t ___interp_invoke_impl_8;
	MethodInfo_t* ___method_info_9;
	MethodInfo_t* ___original_method_info_10;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	int32_t ___method_is_virtual_12;
};

// System.Exception
struct Exception_t  : public RuntimeObject
{
	// System.String System.Exception::_className
	String_t* ____className_1;
	// System.String System.Exception::_message
	String_t* ____message_2;
	// System.Collections.IDictionary System.Exception::_data
	RuntimeObject* ____data_3;
	// System.Exception System.Exception::_innerException
	Exception_t* ____innerException_4;
	// System.String System.Exception::_helpURL
	String_t* ____helpURL_5;
	// System.Object System.Exception::_stackTrace
	RuntimeObject* ____stackTrace_6;
	// System.String System.Exception::_stackTraceString
	String_t* ____stackTraceString_7;
	// System.String System.Exception::_remoteStackTraceString
	String_t* ____remoteStackTraceString_8;
	// System.Int32 System.Exception::_remoteStackIndex
	int32_t ____remoteStackIndex_9;
	// System.Object System.Exception::_dynamicMethods
	RuntimeObject* ____dynamicMethods_10;
	// System.Int32 System.Exception::_HResult
	int32_t ____HResult_11;
	// System.String System.Exception::_source
	String_t* ____source_12;
	// System.Runtime.Serialization.SafeSerializationManager System.Exception::_safeSerializationManager
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager_13;
	// System.Diagnostics.StackTrace[] System.Exception::captured_traces
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces_14;
	// System.IntPtr[] System.Exception::native_trace_ips
	IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832* ___native_trace_ips_15;
	// System.Int32 System.Exception::caught_in_unmanaged
	int32_t ___caught_in_unmanaged_16;
};

struct Exception_t_StaticFields
{
	// System.Object System.Exception::s_EDILock
	RuntimeObject* ___s_EDILock_0;
};
// Native definition for P/Invoke marshalling of System.Exception
struct Exception_t_marshaled_pinvoke
{
	char* ____className_1;
	char* ____message_2;
	RuntimeObject* ____data_3;
	Exception_t_marshaled_pinvoke* ____innerException_4;
	char* ____helpURL_5;
	Il2CppIUnknown* ____stackTrace_6;
	char* ____stackTraceString_7;
	char* ____remoteStackTraceString_8;
	int32_t ____remoteStackIndex_9;
	Il2CppIUnknown* ____dynamicMethods_10;
	int32_t ____HResult_11;
	char* ____source_12;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager_13;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces_14;
	Il2CppSafeArray/*NONE*/* ___native_trace_ips_15;
	int32_t ___caught_in_unmanaged_16;
};
// Native definition for COM marshalling of System.Exception
struct Exception_t_marshaled_com
{
	Il2CppChar* ____className_1;
	Il2CppChar* ____message_2;
	RuntimeObject* ____data_3;
	Exception_t_marshaled_com* ____innerException_4;
	Il2CppChar* ____helpURL_5;
	Il2CppIUnknown* ____stackTrace_6;
	Il2CppChar* ____stackTraceString_7;
	Il2CppChar* ____remoteStackTraceString_8;
	int32_t ____remoteStackIndex_9;
	Il2CppIUnknown* ____dynamicMethods_10;
	int32_t ____HResult_11;
	Il2CppChar* ____source_12;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager_13;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces_14;
	Il2CppSafeArray/*NONE*/* ___native_trace_ips_15;
	int32_t ___caught_in_unmanaged_16;
};

// UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C  : public RuntimeObject
{
	// System.IntPtr UnityEngine.Object::m_CachedPtr
	intptr_t ___m_CachedPtr_0;
};

struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_StaticFields
{
	// System.Int32 UnityEngine.Object::OffsetOfInstanceIDInCPlusPlusObject
	int32_t ___OffsetOfInstanceIDInCPlusPlusObject_1;
};
// Native definition for P/Invoke marshalling of UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke
{
	intptr_t ___m_CachedPtr_0;
};
// Native definition for COM marshalling of UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com
{
	intptr_t ___m_CachedPtr_0;
};

// System.RuntimeTypeHandle
struct RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B 
{
	// System.IntPtr System.RuntimeTypeHandle::value
	intptr_t ___value_0;
};

// UnityEngine.Component
struct Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};

// System.MulticastDelegate
struct MulticastDelegate_t  : public Delegate_t
{
	// System.Delegate[] System.MulticastDelegate::delegates
	DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* ___delegates_13;
};
// Native definition for P/Invoke marshalling of System.MulticastDelegate
struct MulticastDelegate_t_marshaled_pinvoke : public Delegate_t_marshaled_pinvoke
{
	Delegate_t_marshaled_pinvoke** ___delegates_13;
};
// Native definition for COM marshalling of System.MulticastDelegate
struct MulticastDelegate_t_marshaled_com : public Delegate_t_marshaled_com
{
	Delegate_t_marshaled_com** ___delegates_13;
};

// System.SystemException
struct SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295  : public Exception_t
{
};

// System.Type
struct Type_t  : public MemberInfo_t
{
	// System.RuntimeTypeHandle System.Type::_impl
	RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ____impl_8;
};

struct Type_t_StaticFields
{
	// System.Reflection.Binder modreq(System.Runtime.CompilerServices.IsVolatile) System.Type::s_defaultBinder
	Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235* ___s_defaultBinder_0;
	// System.Char System.Type::Delimiter
	Il2CppChar ___Delimiter_1;
	// System.Type[] System.Type::EmptyTypes
	TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB* ___EmptyTypes_2;
	// System.Object System.Type::Missing
	RuntimeObject* ___Missing_3;
	// System.Reflection.MemberFilter System.Type::FilterAttribute
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterAttribute_4;
	// System.Reflection.MemberFilter System.Type::FilterName
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterName_5;
	// System.Reflection.MemberFilter System.Type::FilterNameIgnoreCase
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterNameIgnoreCase_6;
};

// UnityEngine.Behaviour
struct Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA  : public Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3
{
};

// System.InvalidOperationException
struct InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
};

// UnityEngine.RemoteSettings/UpdatedEventHandler
struct UpdatedEventHandler_tB0D5A5BA322FE093894992C29DCF51E7E12579C4  : public MulticastDelegate_t
{
};

// UnityEngine.MonoBehaviour
struct MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71  : public Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA
{
};

// UnityEngine.Analytics.RemoteSettings
struct RemoteSettings_tBBDE520F6CF4BC18914F1A4A058CF33ECC003F75  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
	// UnityEngine.Analytics.DriveableProperty UnityEngine.Analytics.RemoteSettings::m_DriveableProperty
	DriveableProperty_tC1D2C98D55A7EB8BDC2F9E8F0EBB78888D4AD158* ___m_DriveableProperty_4;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif
// System.String[]
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248  : public RuntimeArray
{
	ALIGN_FIELD (8) String_t* m_Items[1];

	inline String_t* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline String_t** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, String_t* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline String_t* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline String_t** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, String_t* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
// System.Object[]
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918  : public RuntimeArray
{
	ALIGN_FIELD (8) RuntimeObject* m_Items[1];

	inline RuntimeObject* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline RuntimeObject** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, RuntimeObject* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline RuntimeObject* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline RuntimeObject** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, RuntimeObject* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
// System.Reflection.ParameterInfo[]
struct ParameterInfoU5BU5D_t86995AB4A1693393FE29B058CC3FD727DF0B984C  : public RuntimeArray
{
	ALIGN_FIELD (8) ParameterInfo_tBC2D68304851A59EFB2EAE6B168714CD45445F2F* m_Items[1];

	inline ParameterInfo_tBC2D68304851A59EFB2EAE6B168714CD45445F2F* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline ParameterInfo_tBC2D68304851A59EFB2EAE6B168714CD45445F2F** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, ParameterInfo_tBC2D68304851A59EFB2EAE6B168714CD45445F2F* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline ParameterInfo_tBC2D68304851A59EFB2EAE6B168714CD45445F2F* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline ParameterInfo_tBC2D68304851A59EFB2EAE6B168714CD45445F2F** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, ParameterInfo_tBC2D68304851A59EFB2EAE6B168714CD45445F2F* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
// System.Char[]
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB  : public RuntimeArray
{
	ALIGN_FIELD (8) Il2CppChar m_Items[1];

	inline Il2CppChar GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Il2CppChar* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Il2CppChar value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline Il2CppChar GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Il2CppChar* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Il2CppChar value)
	{
		m_Items[index] = value;
	}
};


// T System.Collections.Generic.List`1<System.Object>::get_Item(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_get_Item_m33561245D64798C2AB07584C0EC4F240E4839A38_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, int32_t ___index0, const RuntimeMethod* method) ;
// System.Int32 System.Collections.Generic.List`1<System.Object>::get_Count()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t List_1_get_Count_m4407E4C389F22B8CEC282C15D56516658746C383_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) ;

// System.Void System.Object::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
// System.Int32 System.String::get_Length()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline (String_t* __this, const RuntimeMethod* method) ;
// System.String[] System.String::Split(System.Char[],System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* String_Split_m3C63FA89A52BE352B4E49DB5379F7AAD6ACCA0E8 (String_t* __this, CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* ___separator0, int32_t ___count1, const RuntimeMethod* method) ;
// System.Type System.Object::GetType()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Type_t* Object_GetType_mE10A8FC1E57F3DF29972CCBC026C2DC3942263B3 (RuntimeObject* __this, const RuntimeMethod* method) ;
// System.Reflection.FieldInfo System.Type::GetField(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR FieldInfo_t* Type_GetField_m0BF55B1A27A1B6AB6D3477E7F9E1CF2A3451E1E0 (Type_t* __this, String_t* ___name0, const RuntimeMethod* method) ;
// System.Reflection.PropertyInfo System.Type::GetProperty(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PropertyInfo_t* Type_GetProperty_mD183124FC8A89121E8368058B327A7750B14281D (Type_t* __this, String_t* ___name0, const RuntimeMethod* method) ;
// System.Boolean System.Reflection.FieldInfo::op_Inequality(System.Reflection.FieldInfo,System.Reflection.FieldInfo)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool FieldInfo_op_Inequality_m95789A98E646494987E66A9E4188DCA86185066B (FieldInfo_t* ___left0, FieldInfo_t* ___right1, const RuntimeMethod* method) ;
// System.Boolean System.Reflection.PropertyInfo::op_Inequality(System.Reflection.PropertyInfo,System.Reflection.PropertyInfo)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool PropertyInfo_op_Inequality_mE75A4F14CC678D8A670730FBD4338C718CACB51B (PropertyInfo_t* ___left0, PropertyInfo_t* ___right1, const RuntimeMethod* method) ;
// System.Int32 System.Array::GetLength(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_GetLength_mFE7A9FE891DE1E07795230BE09854441CDD0E935 (RuntimeArray* __this, int32_t ___dimension0, const RuntimeMethod* method) ;
// System.Type System.Type::GetTypeFromHandle(System.RuntimeTypeHandle)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Type_t* Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57 (RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ___handle0, const RuntimeMethod* method) ;
// System.Boolean System.Type::op_Equality(System.Type,System.Type)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Type_op_Equality_m99930A0E44E420A685FABA60E60BA1CC5FA0EBDC (Type_t* ___left0, Type_t* ___right1, const RuntimeMethod* method) ;
// System.Boolean System.Int32::TryParse(System.String,System.Int32&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Int32_TryParse_mC928DE2FEC1C35ED5298BDDCA9868076E94B8A21 (String_t* ___s0, int32_t* ___result1, const RuntimeMethod* method) ;
// System.String System.String::Format(System.String,System.Object,System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987 (String_t* ___format0, RuntimeObject* ___arg01, RuntimeObject* ___arg12, const RuntimeMethod* method) ;
// System.Void System.InvalidOperationException::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162 (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* __this, String_t* ___message0, const RuntimeMethod* method) ;
// System.Object UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey::SetValueRecursive(System.Object,System.Object,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* FieldWithRemoteSettingsKey_SetValueRecursive_m0E72D0080DC0B6AA5BA0C9E4861355CCF83B177A (FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* __this, RuntimeObject* ___val0, RuntimeObject* ___target1, String_t* ___path2, const RuntimeMethod* method) ;
// System.Boolean System.Reflection.FieldInfo::get_IsInitOnly()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool FieldInfo_get_IsInitOnly_m476BB9325A68BDD56B088D3E8407F75FA1388ED9 (FieldInfo_t* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.Debug::LogWarning(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9 (RuntimeObject* ___message0, const RuntimeMethod* method) ;
// System.Void System.Reflection.FieldInfo::SetValue(System.Object,System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FieldInfo_SetValue_mD8C0DA3A1A0CFF073F971622BBDBAAB6688B4B6C (FieldInfo_t* __this, RuntimeObject* ___obj0, RuntimeObject* ___value1, const RuntimeMethod* method) ;
// System.Boolean System.Type::get_IsValueType()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Type_get_IsValueType_m59AE2E0439DC06347B8D6B38548F3CBA54D38318 (Type_t* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.Analytics.RemoteSettings::RemoteSettingsUpdated()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RemoteSettings_RemoteSettingsUpdated_m69908D5B790A05F22B83F316806CE30D32F0E1B3 (RemoteSettings_tBBDE520F6CF4BC18914F1A4A058CF33ECC003F75* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.RemoteSettings/UpdatedEventHandler::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UpdatedEventHandler__ctor_mB914409481F8FDC738B4EDB1DBB4883F743F863A (UpdatedEventHandler_tB0D5A5BA322FE093894992C29DCF51E7E12579C4* __this, RuntimeObject* ___object0, intptr_t ___method1, const RuntimeMethod* method) ;
// System.Void UnityEngine.RemoteSettings::add_Updated(UnityEngine.RemoteSettings/UpdatedEventHandler)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RemoteSettings_add_Updated_m236B4D6AAF3342720696E96A252AAD9956B84A72 (UpdatedEventHandler_tB0D5A5BA322FE093894992C29DCF51E7E12579C4* ___value0, const RuntimeMethod* method) ;
// System.Collections.Generic.List`1<UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey> UnityEngine.Analytics.DriveableProperty::get_fields()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR List_1_tCD3B17EC7F519BA836765E50183BF8C97D94302A* DriveableProperty_get_fields_m9554FDEAB0CCB85D415E193F70395A69E2741CA2_inline (DriveableProperty_tC1D2C98D55A7EB8BDC2F9E8F0EBB78888D4AD158* __this, const RuntimeMethod* method) ;
// T System.Collections.Generic.List`1<UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey>::get_Item(System.Int32)
inline FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* List_1_get_Item_m3599DA91AE71D5F6EBE6CFFEBC26A75B63F57D91 (List_1_tCD3B17EC7F519BA836765E50183BF8C97D94302A* __this, int32_t ___index0, const RuntimeMethod* method)
{
	return ((  FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* (*) (List_1_tCD3B17EC7F519BA836765E50183BF8C97D94302A*, int32_t, const RuntimeMethod*))List_1_get_Item_m33561245D64798C2AB07584C0EC4F240E4839A38_gshared)(__this, ___index0, method);
}
// System.String UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey::get_rsKeyName()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* FieldWithRemoteSettingsKey_get_rsKeyName_m19EE15BFF91980A5BADA0AC93848010EDCFDA9CD_inline (FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* __this, const RuntimeMethod* method) ;
// System.Boolean System.String::IsNullOrEmpty(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool String_IsNullOrEmpty_mEA9E3FB005AC28FE02E69FCF95A7B8456192B478 (String_t* ___value0, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.RemoteSettings::HasKey(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool RemoteSettings_HasKey_mB1DB6707317744D3FC2219B7624F1471328BE3C6 (String_t* ___key0, const RuntimeMethod* method) ;
// UnityEngine.Object UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey::get_target()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* FieldWithRemoteSettingsKey_get_target_mD39F72EB38D8BAAA991F8CB235B297BC0DDA799F_inline (FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* __this, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Object::op_Inequality(UnityEngine.Object,UnityEngine.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Object_op_Inequality_mD0BE578448EAA61948F25C32F8DD55AB1F778602 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___x0, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___y1, const RuntimeMethod* method) ;
// System.String UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey::get_fieldPath()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* FieldWithRemoteSettingsKey_get_fieldPath_m5E82F5C7159D1C9E71CE01C0C10F848C7FC1FABD_inline (FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* __this, const RuntimeMethod* method) ;
// System.String UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey::get_type()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* FieldWithRemoteSettingsKey_get_type_mA089AE382B7C2233B18ECCD87E750BBC5CAD98CD_inline (FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* __this, const RuntimeMethod* method) ;
// System.Boolean System.String::op_Equality(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1 (String_t* ___a0, String_t* ___b1, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.RemoteSettings::GetBool(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool RemoteSettings_GetBool_m34D0B037018CA55A2C7EDAD2121864B28A9BAC78 (String_t* ___key0, const RuntimeMethod* method) ;
// System.Void UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey::SetValue(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FieldWithRemoteSettingsKey_SetValue_m2CF14A7D7173DEDAEB81D2CA0B4DB87D562FF279 (FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* __this, RuntimeObject* ___val0, const RuntimeMethod* method) ;
// System.Single UnityEngine.RemoteSettings::GetFloat(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float RemoteSettings_GetFloat_m4062B14E0CF358581AC256B668F74BB542777807 (String_t* ___key0, const RuntimeMethod* method) ;
// System.Int32 UnityEngine.RemoteSettings::GetInt(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t RemoteSettings_GetInt_m6B2CCA75F70E2052BC562E2F6C2D91F1CB66335E (String_t* ___key0, const RuntimeMethod* method) ;
// System.String UnityEngine.RemoteSettings::GetString(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* RemoteSettings_GetString_mD41BAF75A8DD0F38CD78B4AE26E22D3D594B337C (String_t* ___key0, const RuntimeMethod* method) ;
// System.Int32 System.Collections.Generic.List`1<UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey>::get_Count()
inline int32_t List_1_get_Count_mF20CB9BE944B8F7DA3107E94690C3A7D29E3CB4A_inline (List_1_tCD3B17EC7F519BA836765E50183BF8C97D94302A* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_tCD3B17EC7F519BA836765E50183BF8C97D94302A*, const RuntimeMethod*))List_1_get_Count_m4407E4C389F22B8CEC282C15D56516658746C383_gshared_inline)(__this, method);
}
// System.Void UnityEngine.Analytics.DriveableProperty::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DriveableProperty__ctor_mF0E622CFD914ADE0C3DAEAB06A4DCEB71F7A9B69 (DriveableProperty_tC1D2C98D55A7EB8BDC2F9E8F0EBB78888D4AD158* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.MonoBehaviour::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E (MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71* __this, const RuntimeMethod* method) ;
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Collections.Generic.List`1<UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey> UnityEngine.Analytics.DriveableProperty::get_fields()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR List_1_tCD3B17EC7F519BA836765E50183BF8C97D94302A* DriveableProperty_get_fields_m9554FDEAB0CCB85D415E193F70395A69E2741CA2 (DriveableProperty_tC1D2C98D55A7EB8BDC2F9E8F0EBB78888D4AD158* __this, const RuntimeMethod* method) 
{
	{
		// get { return m_Fields; }
		List_1_tCD3B17EC7F519BA836765E50183BF8C97D94302A* L_0 = __this->___m_Fields_0;
		return L_0;
	}
}
// System.Void UnityEngine.Analytics.DriveableProperty::set_fields(System.Collections.Generic.List`1<UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DriveableProperty_set_fields_m76F701F5AAEF9852CF2120972374FCF28722C7CD (DriveableProperty_tC1D2C98D55A7EB8BDC2F9E8F0EBB78888D4AD158* __this, List_1_tCD3B17EC7F519BA836765E50183BF8C97D94302A* ___value0, const RuntimeMethod* method) 
{
	{
		// set { m_Fields = value; }
		List_1_tCD3B17EC7F519BA836765E50183BF8C97D94302A* L_0 = ___value0;
		__this->___m_Fields_0 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_Fields_0), (void*)L_0);
		// set { m_Fields = value; }
		return;
	}
}
// System.Void UnityEngine.Analytics.DriveableProperty::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DriveableProperty__ctor_mF0E622CFD914ADE0C3DAEAB06A4DCEB71F7A9B69 (DriveableProperty_tC1D2C98D55A7EB8BDC2F9E8F0EBB78888D4AD158* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// UnityEngine.Object UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey::get_target()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* FieldWithRemoteSettingsKey_get_target_mD39F72EB38D8BAAA991F8CB235B297BC0DDA799F (FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* __this, const RuntimeMethod* method) 
{
	{
		// get { return m_Target; }
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_0 = __this->___m_Target_0;
		return L_0;
	}
}
// System.Void UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey::set_target(UnityEngine.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FieldWithRemoteSettingsKey_set_target_mB55A3B15CA8051C3A80F14452DF18778E85A420D (FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* __this, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___value0, const RuntimeMethod* method) 
{
	{
		// set { m_Target = value; }
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_0 = ___value0;
		__this->___m_Target_0 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_Target_0), (void*)L_0);
		// set { m_Target = value; }
		return;
	}
}
// System.String UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey::get_fieldPath()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* FieldWithRemoteSettingsKey_get_fieldPath_m5E82F5C7159D1C9E71CE01C0C10F848C7FC1FABD (FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* __this, const RuntimeMethod* method) 
{
	{
		// get { return m_FieldPath; }
		String_t* L_0 = __this->___m_FieldPath_1;
		return L_0;
	}
}
// System.Void UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey::set_fieldPath(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FieldWithRemoteSettingsKey_set_fieldPath_mA69F42CF349CF7F65CD5C0F694107DE5C399A325 (FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* __this, String_t* ___value0, const RuntimeMethod* method) 
{
	{
		// set { m_FieldPath = value; }
		String_t* L_0 = ___value0;
		__this->___m_FieldPath_1 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_FieldPath_1), (void*)L_0);
		// set { m_FieldPath = value; }
		return;
	}
}
// System.String UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey::get_rsKeyName()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* FieldWithRemoteSettingsKey_get_rsKeyName_m19EE15BFF91980A5BADA0AC93848010EDCFDA9CD (FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* __this, const RuntimeMethod* method) 
{
	{
		// get { return m_RSKeyName; }
		String_t* L_0 = __this->___m_RSKeyName_2;
		return L_0;
	}
}
// System.Void UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey::set_rsKeyName(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FieldWithRemoteSettingsKey_set_rsKeyName_mE0C3E077DF263212171CCF8FED6FA0C22864E5BF (FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* __this, String_t* ___value0, const RuntimeMethod* method) 
{
	{
		// set { m_RSKeyName = value; }
		String_t* L_0 = ___value0;
		__this->___m_RSKeyName_2 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_RSKeyName_2), (void*)L_0);
		// set { m_RSKeyName = value; }
		return;
	}
}
// System.String UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey::get_type()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* FieldWithRemoteSettingsKey_get_type_mA089AE382B7C2233B18ECCD87E750BBC5CAD98CD (FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* __this, const RuntimeMethod* method) 
{
	{
		// get { return m_Type; }
		String_t* L_0 = __this->___m_Type_3;
		return L_0;
	}
}
// System.Void UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey::set_type(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FieldWithRemoteSettingsKey_set_type_mE17683A693242E1B71938DFA29CADAB75EF4762E (FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* __this, String_t* ___value0, const RuntimeMethod* method) 
{
	{
		// set { m_Type = value; }
		String_t* L_0 = ___value0;
		__this->___m_Type_3 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_Type_3), (void*)L_0);
		// set { m_Type = value; }
		return;
	}
}
// System.Object UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey::SetValueRecursive(System.Object,System.Object,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* FieldWithRemoteSettingsKey_SetValueRecursive_m0E72D0080DC0B6AA5BA0C9E4861355CCF83B177A (FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* __this, RuntimeObject* ___val0, RuntimeObject* ___target1, String_t* ___path2, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_0_0_0_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Type_t_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralF5710B540AE7F103C278A568381FDD4AF48934CA);
		s_Il2CppMethodInitialized = true;
	}
	StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* V_0 = NULL;
	String_t* V_1 = NULL;
	String_t* V_2 = NULL;
	FieldInfo_t* V_3 = NULL;
	PropertyInfo_t* V_4 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* V_5 = NULL;
	RuntimeObject* V_6 = NULL;
	RuntimeObject* V_7 = NULL;
	ParameterInfoU5BU5D_t86995AB4A1693393FE29B058CC3FD727DF0B984C* V_8 = NULL;
	int32_t V_9 = 0;
	String_t* G_B5_0 = NULL;
	String_t* G_B15_0 = NULL;
	{
		// if (path.Length == 0)
		String_t* L_0 = ___path2;
		int32_t L_1;
		L_1 = String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline(L_0, NULL);
		if (L_1)
		{
			goto IL_000a;
		}
	}
	{
		// return val;
		RuntimeObject* L_2 = ___val0;
		return L_2;
	}

IL_000a:
	{
		// string[] split = path.Split(new char[] {'.'}, 2);
		String_t* L_3 = ___path2;
		CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* L_4 = (CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB*)(CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB*)SZArrayNew(CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB_il2cpp_TypeInfo_var, (uint32_t)1);
		CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* L_5 = L_4;
		(L_5)->SetAtUnchecked(static_cast<il2cpp_array_size_t>(0), (Il2CppChar)((int32_t)46));
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_6;
		L_6 = String_Split_m3C63FA89A52BE352B4E49DB5379F7AAD6ACCA0E8(L_3, L_5, 2, NULL);
		V_0 = L_6;
		// string next = split[0];
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_7 = V_0;
		int32_t L_8 = 0;
		String_t* L_9 = (L_7)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_8));
		V_1 = L_9;
		// string rest = split.Length > 1 ? split[1] : "";
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_10 = V_0;
		if ((((int32_t)((int32_t)(((RuntimeArray*)L_10)->max_length))) > ((int32_t)1)))
		{
			goto IL_002e;
		}
	}
	{
		G_B5_0 = _stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709;
		goto IL_0031;
	}

IL_002e:
	{
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_11 = V_0;
		int32_t L_12 = 1;
		String_t* L_13 = (L_11)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_12));
		G_B5_0 = L_13;
	}

IL_0031:
	{
		V_2 = G_B5_0;
		// Type targetType = target.GetType();
		RuntimeObject* L_14 = ___target1;
		Type_t* L_15;
		L_15 = Object_GetType_mE10A8FC1E57F3DF29972CCBC026C2DC3942263B3(L_14, NULL);
		// var field = targetType.GetField(next);
		Type_t* L_16 = L_15;
		String_t* L_17 = V_1;
		FieldInfo_t* L_18;
		L_18 = Type_GetField_m0BF55B1A27A1B6AB6D3477E7F9E1CF2A3451E1E0(L_16, L_17, NULL);
		V_3 = L_18;
		// var prop = targetType.GetProperty(next);
		String_t* L_19 = V_1;
		PropertyInfo_t* L_20;
		L_20 = Type_GetProperty_mD183124FC8A89121E8368058B327A7750B14281D(L_16, L_19, NULL);
		V_4 = L_20;
		// object[] parameters = null;
		V_5 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)NULL;
		// if (field != null)
		FieldInfo_t* L_21 = V_3;
		bool L_22;
		L_22 = FieldInfo_op_Inequality_m95789A98E646494987E66A9E4188DCA86185066B(L_21, (FieldInfo_t*)NULL, NULL);
		if (!L_22)
		{
			goto IL_0062;
		}
	}
	{
		// newTarget = field.GetValue(target);
		FieldInfo_t* L_23 = V_3;
		RuntimeObject* L_24 = ___target1;
		RuntimeObject* L_25;
		L_25 = VirtualFuncInvoker1< RuntimeObject*, RuntimeObject* >::Invoke(22 /* System.Object System.Reflection.FieldInfo::GetValue(System.Object) */, L_23, L_24);
		V_6 = L_25;
		goto IL_0104;
	}

IL_0062:
	{
		// else if (prop != null)
		PropertyInfo_t* L_26 = V_4;
		bool L_27;
		L_27 = PropertyInfo_op_Inequality_mE75A4F14CC678D8A670730FBD4338C718CACB51B(L_26, (PropertyInfo_t*)NULL, NULL);
		if (!L_27)
		{
			goto IL_00f2;
		}
	}
	{
		// var indexParams = prop.GetIndexParameters();
		PropertyInfo_t* L_28 = V_4;
		ParameterInfoU5BU5D_t86995AB4A1693393FE29B058CC3FD727DF0B984C* L_29;
		L_29 = VirtualFuncInvoker0< ParameterInfoU5BU5D_t86995AB4A1693393FE29B058CC3FD727DF0B984C* >::Invoke(16 /* System.Reflection.ParameterInfo[] System.Reflection.PropertyInfo::GetIndexParameters() */, L_28);
		V_8 = L_29;
		// if (indexParams.GetLength(0) == 1 && indexParams[0].ParameterType == typeof(int))
		ParameterInfoU5BU5D_t86995AB4A1693393FE29B058CC3FD727DF0B984C* L_30 = V_8;
		int32_t L_31;
		L_31 = Array_GetLength_mFE7A9FE891DE1E07795230BE09854441CDD0E935((RuntimeArray*)L_30, 0, NULL);
		if ((!(((uint32_t)L_31) == ((uint32_t)1))))
		{
			goto IL_00e4;
		}
	}
	{
		ParameterInfoU5BU5D_t86995AB4A1693393FE29B058CC3FD727DF0B984C* L_32 = V_8;
		int32_t L_33 = 0;
		ParameterInfo_tBC2D68304851A59EFB2EAE6B168714CD45445F2F* L_34 = (L_32)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_33));
		Type_t* L_35;
		L_35 = VirtualFuncInvoker0< Type_t* >::Invoke(10 /* System.Type System.Reflection.ParameterInfo::get_ParameterType() */, L_34);
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_36 = { reinterpret_cast<intptr_t> (Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_0_0_0_var) };
		il2cpp_codegen_runtime_class_init_inline(Type_t_il2cpp_TypeInfo_var);
		Type_t* L_37;
		L_37 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_36, NULL);
		bool L_38;
		L_38 = Type_op_Equality_m99930A0E44E420A685FABA60E60BA1CC5FA0EBDC(L_35, L_37, NULL);
		if (!L_38)
		{
			goto IL_00e4;
		}
	}
	{
		// split = rest.Split(new char[] { '.' }, 2);
		String_t* L_39 = V_2;
		CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* L_40 = (CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB*)(CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB*)SZArrayNew(CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB_il2cpp_TypeInfo_var, (uint32_t)1);
		CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* L_41 = L_40;
		(L_41)->SetAtUnchecked(static_cast<il2cpp_array_size_t>(0), (Il2CppChar)((int32_t)46));
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_42;
		L_42 = String_Split_m3C63FA89A52BE352B4E49DB5379F7AAD6ACCA0E8(L_39, L_41, 2, NULL);
		V_0 = L_42;
		// if (split[0] != null && Int32.TryParse(split[0], out index))
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_43 = V_0;
		int32_t L_44 = 0;
		String_t* L_45 = (L_43)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_44));
		if (!L_45)
		{
			goto IL_00e4;
		}
	}
	{
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_46 = V_0;
		int32_t L_47 = 0;
		String_t* L_48 = (L_46)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_47));
		bool L_49;
		L_49 = Int32_TryParse_mC928DE2FEC1C35ED5298BDDCA9868076E94B8A21(L_48, (&V_9), NULL);
		if (!L_49)
		{
			goto IL_00e4;
		}
	}
	{
		// parameters = new object[] { index };
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_50 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)1);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_51 = L_50;
		int32_t L_52 = V_9;
		int32_t L_53 = L_52;
		RuntimeObject* L_54 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_53);
		ArrayElementTypeCheck (L_51, L_54);
		(L_51)->SetAtUnchecked(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_54);
		V_5 = L_51;
		// rest = split.Length > 1 ? split[1] : "";
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_55 = V_0;
		if ((((int32_t)((int32_t)(((RuntimeArray*)L_55)->max_length))) > ((int32_t)1)))
		{
			goto IL_00e0;
		}
	}
	{
		G_B15_0 = _stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709;
		goto IL_00e3;
	}

IL_00e0:
	{
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_56 = V_0;
		int32_t L_57 = 1;
		String_t* L_58 = (L_56)->GetAtUnchecked(static_cast<il2cpp_array_size_t>(L_57));
		G_B15_0 = L_58;
	}

IL_00e3:
	{
		V_2 = G_B15_0;
	}

IL_00e4:
	{
		// newTarget = prop.GetValue(target, parameters);
		PropertyInfo_t* L_59 = V_4;
		RuntimeObject* L_60 = ___target1;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_61 = V_5;
		RuntimeObject* L_62;
		L_62 = VirtualFuncInvoker2< RuntimeObject*, RuntimeObject*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* >::Invoke(20 /* System.Object System.Reflection.PropertyInfo::GetValue(System.Object,System.Object[]) */, L_59, L_60, L_61);
		V_6 = L_62;
		goto IL_0104;
	}

IL_00f2:
	{
		// throw new InvalidOperationException(String.Format("Member '{0}' on target {1} is neither a field nor property", next, target));
		String_t* L_63 = V_1;
		RuntimeObject* L_64 = ___target1;
		String_t* L_65;
		L_65 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralEAFF286356193975BDF7D1E2C8BE0356352A4BF2)), L_63, L_64, NULL);
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_66 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_66, L_65, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_66, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&FieldWithRemoteSettingsKey_SetValueRecursive_m0E72D0080DC0B6AA5BA0C9E4861355CCF83B177A_RuntimeMethod_var)));
	}

IL_0104:
	{
		// var newVal = SetValueRecursive(val, newTarget, rest);
		RuntimeObject* L_67 = ___val0;
		RuntimeObject* L_68 = V_6;
		String_t* L_69 = V_2;
		RuntimeObject* L_70;
		L_70 = FieldWithRemoteSettingsKey_SetValueRecursive_m0E72D0080DC0B6AA5BA0C9E4861355CCF83B177A(__this, L_67, L_68, L_69, NULL);
		V_7 = L_70;
		// if (newVal != null)
		RuntimeObject* L_71 = V_7;
		if (!L_71)
		{
			goto IL_0170;
		}
	}
	{
		// if (field != null)
		FieldInfo_t* L_72 = V_3;
		bool L_73;
		L_73 = FieldInfo_op_Inequality_m95789A98E646494987E66A9E4188DCA86185066B(L_72, (FieldInfo_t*)NULL, NULL);
		if (!L_73)
		{
			goto IL_0147;
		}
	}
	{
		// if (field.IsInitOnly)
		FieldInfo_t* L_74 = V_3;
		bool L_75;
		L_75 = FieldInfo_get_IsInitOnly_m476BB9325A68BDD56B088D3E8407F75FA1388ED9(L_74, NULL);
		if (!L_75)
		{
			goto IL_012f;
		}
	}
	{
		// Debug.LogWarning("You probably shouldn't set a field on a readonly struct even though it works (sometimes)");
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteralF5710B540AE7F103C278A568381FDD4AF48934CA, NULL);
	}

IL_012f:
	{
		// field.SetValue(target, newVal);
		FieldInfo_t* L_76 = V_3;
		RuntimeObject* L_77 = ___target1;
		RuntimeObject* L_78 = V_7;
		FieldInfo_SetValue_mD8C0DA3A1A0CFF073F971622BBDBAAB6688B4B6C(L_76, L_77, L_78, NULL);
		// bool isValueType = target.GetType().IsValueType;
		RuntimeObject* L_79 = ___target1;
		Type_t* L_80;
		L_80 = Object_GetType_mE10A8FC1E57F3DF29972CCBC026C2DC3942263B3(L_79, NULL);
		bool L_81;
		L_81 = Type_get_IsValueType_m59AE2E0439DC06347B8D6B38548F3CBA54D38318(L_80, NULL);
		// if (isValueType)
		if (!L_81)
		{
			goto IL_0170;
		}
	}
	{
		// return target;
		RuntimeObject* L_82 = ___target1;
		return L_82;
	}

IL_0147:
	{
		// if (prop.CanWrite)
		PropertyInfo_t* L_83 = V_4;
		bool L_84;
		L_84 = VirtualFuncInvoker0< bool >::Invoke(17 /* System.Boolean System.Reflection.PropertyInfo::get_CanWrite() */, L_83);
		if (!L_84)
		{
			goto IL_015e;
		}
	}
	{
		// prop.SetValue(target, newVal, parameters);
		PropertyInfo_t* L_85 = V_4;
		RuntimeObject* L_86 = ___target1;
		RuntimeObject* L_87 = V_7;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_88 = V_5;
		VirtualActionInvoker3< RuntimeObject*, RuntimeObject*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* >::Invoke(22 /* System.Void System.Reflection.PropertyInfo::SetValue(System.Object,System.Object,System.Object[]) */, L_85, L_86, L_87, L_88);
		goto IL_0170;
	}

IL_015e:
	{
		// throw new InvalidOperationException(String.Format("Property '{0}' on target {1} is readonly", next, target));
		String_t* L_89 = V_1;
		RuntimeObject* L_90 = ___target1;
		String_t* L_91;
		L_91 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralCDE9DA404E87DC408CE8E83F2431F91A958C9ABB)), L_89, L_90, NULL);
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_92 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_92, L_91, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_92, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&FieldWithRemoteSettingsKey_SetValueRecursive_m0E72D0080DC0B6AA5BA0C9E4861355CCF83B177A_RuntimeMethod_var)));
	}

IL_0170:
	{
		// return null;
		return NULL;
	}
}
// System.Void UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey::SetValue(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FieldWithRemoteSettingsKey_SetValue_m2CF14A7D7173DEDAEB81D2CA0B4DB87D562FF279 (FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* __this, RuntimeObject* ___val0, const RuntimeMethod* method) 
{
	{
		// SetValueRecursive(val, m_Target, m_FieldPath);
		RuntimeObject* L_0 = ___val0;
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_1 = __this->___m_Target_0;
		String_t* L_2 = __this->___m_FieldPath_1;
		RuntimeObject* L_3;
		L_3 = FieldWithRemoteSettingsKey_SetValueRecursive_m0E72D0080DC0B6AA5BA0C9E4861355CCF83B177A(__this, L_0, L_1, L_2, NULL);
		// }
		return;
	}
}
// System.Void UnityEngine.Analytics.DriveableProperty/FieldWithRemoteSettingsKey::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FieldWithRemoteSettingsKey__ctor_m735A7F65AF38A3CA06AEA0FD7DEB190A159CD4B2 (FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// UnityEngine.Analytics.DriveableProperty UnityEngine.Analytics.RemoteSettings::get_driveableProperty()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR DriveableProperty_tC1D2C98D55A7EB8BDC2F9E8F0EBB78888D4AD158* RemoteSettings_get_driveableProperty_mE6D43DA649B9E739C2983C6F3B6736D90CFA793F (RemoteSettings_tBBDE520F6CF4BC18914F1A4A058CF33ECC003F75* __this, const RuntimeMethod* method) 
{
	{
		// get { return m_DriveableProperty; }
		DriveableProperty_tC1D2C98D55A7EB8BDC2F9E8F0EBB78888D4AD158* L_0 = __this->___m_DriveableProperty_4;
		return L_0;
	}
}
// System.Void UnityEngine.Analytics.RemoteSettings::set_driveableProperty(UnityEngine.Analytics.DriveableProperty)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RemoteSettings_set_driveableProperty_m80FE7D178544A5E1CD3B434A40ECCD968BF058E1 (RemoteSettings_tBBDE520F6CF4BC18914F1A4A058CF33ECC003F75* __this, DriveableProperty_tC1D2C98D55A7EB8BDC2F9E8F0EBB78888D4AD158* ___value0, const RuntimeMethod* method) 
{
	{
		// set { m_DriveableProperty = value; }
		DriveableProperty_tC1D2C98D55A7EB8BDC2F9E8F0EBB78888D4AD158* L_0 = ___value0;
		__this->___m_DriveableProperty_4 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_DriveableProperty_4), (void*)L_0);
		// set { m_DriveableProperty = value; }
		return;
	}
}
// System.Void UnityEngine.Analytics.RemoteSettings::Start()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RemoteSettings_Start_m768105650993F32B8115A082C720A8D530EE1693 (RemoteSettings_tBBDE520F6CF4BC18914F1A4A058CF33ECC003F75* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RemoteSettings_RemoteSettingsUpdated_m69908D5B790A05F22B83F316806CE30D32F0E1B3_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UpdatedEventHandler_tB0D5A5BA322FE093894992C29DCF51E7E12579C4_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// RemoteSettingsUpdated();
		RemoteSettings_RemoteSettingsUpdated_m69908D5B790A05F22B83F316806CE30D32F0E1B3(__this, NULL);
		// RS.Updated += RemoteSettingsUpdated;
		UpdatedEventHandler_tB0D5A5BA322FE093894992C29DCF51E7E12579C4* L_0 = (UpdatedEventHandler_tB0D5A5BA322FE093894992C29DCF51E7E12579C4*)il2cpp_codegen_object_new(UpdatedEventHandler_tB0D5A5BA322FE093894992C29DCF51E7E12579C4_il2cpp_TypeInfo_var);
		UpdatedEventHandler__ctor_mB914409481F8FDC738B4EDB1DBB4883F743F863A(L_0, __this, (intptr_t)((void*)RemoteSettings_RemoteSettingsUpdated_m69908D5B790A05F22B83F316806CE30D32F0E1B3_RuntimeMethod_var), NULL);
		RemoteSettings_add_Updated_m236B4D6AAF3342720696E96A252AAD9956B84A72(L_0, NULL);
		// }
		return;
	}
}
// System.Void UnityEngine.Analytics.RemoteSettings::RemoteSettingsUpdated()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RemoteSettings_RemoteSettingsUpdated_m69908D5B790A05F22B83F316806CE30D32F0E1B3 (RemoteSettings_tBBDE520F6CF4BC18914F1A4A058CF33ECC003F75* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mF20CB9BE944B8F7DA3107E94690C3A7D29E3CB4A_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Item_m3599DA91AE71D5F6EBE6CFFEBC26A75B63F57D91_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral0BA8CB3B900ECEF5E697192B8CDA6B626EB0CE72);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2F1705A1AA8BA6FCE863E7F2CBA4BC28458C77AE);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral673CC9996FD90AFE21BD8D0E6E6824353AF4BDA2);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralE3DFC065B6A6D9931B797808DD066491AAB82B29);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* V_1 = NULL;
	{
		// for (int i = 0; i < m_DriveableProperty.fields.Count; i++)
		V_0 = 0;
		goto IL_00ff;
	}

IL_0007:
	{
		// var f = m_DriveableProperty.fields[i];
		DriveableProperty_tC1D2C98D55A7EB8BDC2F9E8F0EBB78888D4AD158* L_0 = __this->___m_DriveableProperty_4;
		List_1_tCD3B17EC7F519BA836765E50183BF8C97D94302A* L_1;
		L_1 = DriveableProperty_get_fields_m9554FDEAB0CCB85D415E193F70395A69E2741CA2_inline(L_0, NULL);
		int32_t L_2 = V_0;
		FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* L_3;
		L_3 = List_1_get_Item_m3599DA91AE71D5F6EBE6CFFEBC26A75B63F57D91(L_1, L_2, List_1_get_Item_m3599DA91AE71D5F6EBE6CFFEBC26A75B63F57D91_RuntimeMethod_var);
		V_1 = L_3;
		// if (!string.IsNullOrEmpty(f.rsKeyName) && RS.HasKey(f.rsKeyName) && f.target != null && !string.IsNullOrEmpty(f.fieldPath))
		FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* L_4 = V_1;
		String_t* L_5;
		L_5 = FieldWithRemoteSettingsKey_get_rsKeyName_m19EE15BFF91980A5BADA0AC93848010EDCFDA9CD_inline(L_4, NULL);
		bool L_6;
		L_6 = String_IsNullOrEmpty_mEA9E3FB005AC28FE02E69FCF95A7B8456192B478(L_5, NULL);
		if (L_6)
		{
			goto IL_00fb;
		}
	}
	{
		FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* L_7 = V_1;
		String_t* L_8;
		L_8 = FieldWithRemoteSettingsKey_get_rsKeyName_m19EE15BFF91980A5BADA0AC93848010EDCFDA9CD_inline(L_7, NULL);
		bool L_9;
		L_9 = RemoteSettings_HasKey_mB1DB6707317744D3FC2219B7624F1471328BE3C6(L_8, NULL);
		if (!L_9)
		{
			goto IL_00fb;
		}
	}
	{
		FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* L_10 = V_1;
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_11;
		L_11 = FieldWithRemoteSettingsKey_get_target_mD39F72EB38D8BAAA991F8CB235B297BC0DDA799F_inline(L_10, NULL);
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_12;
		L_12 = Object_op_Inequality_mD0BE578448EAA61948F25C32F8DD55AB1F778602(L_11, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_12)
		{
			goto IL_00fb;
		}
	}
	{
		FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* L_13 = V_1;
		String_t* L_14;
		L_14 = FieldWithRemoteSettingsKey_get_fieldPath_m5E82F5C7159D1C9E71CE01C0C10F848C7FC1FABD_inline(L_13, NULL);
		bool L_15;
		L_15 = String_IsNullOrEmpty_mEA9E3FB005AC28FE02E69FCF95A7B8456192B478(L_14, NULL);
		if (L_15)
		{
			goto IL_00fb;
		}
	}
	{
		// if (f.type == "bool")
		FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* L_16 = V_1;
		String_t* L_17;
		L_17 = FieldWithRemoteSettingsKey_get_type_mA089AE382B7C2233B18ECCD87E750BBC5CAD98CD_inline(L_16, NULL);
		bool L_18;
		L_18 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_17, _stringLiteral673CC9996FD90AFE21BD8D0E6E6824353AF4BDA2, NULL);
		if (!L_18)
		{
			goto IL_0084;
		}
	}
	{
		// f.SetValue(RS.GetBool(f.rsKeyName));
		FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* L_19 = V_1;
		FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* L_20 = V_1;
		String_t* L_21;
		L_21 = FieldWithRemoteSettingsKey_get_rsKeyName_m19EE15BFF91980A5BADA0AC93848010EDCFDA9CD_inline(L_20, NULL);
		bool L_22;
		L_22 = RemoteSettings_GetBool_m34D0B037018CA55A2C7EDAD2121864B28A9BAC78(L_21, NULL);
		bool L_23 = L_22;
		RuntimeObject* L_24 = Box(Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, &L_23);
		FieldWithRemoteSettingsKey_SetValue_m2CF14A7D7173DEDAEB81D2CA0B4DB87D562FF279(L_19, L_24, NULL);
		goto IL_00fb;
	}

IL_0084:
	{
		// else if (f.type == "float")
		FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* L_25 = V_1;
		String_t* L_26;
		L_26 = FieldWithRemoteSettingsKey_get_type_mA089AE382B7C2233B18ECCD87E750BBC5CAD98CD_inline(L_25, NULL);
		bool L_27;
		L_27 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_26, _stringLiteralE3DFC065B6A6D9931B797808DD066491AAB82B29, NULL);
		if (!L_27)
		{
			goto IL_00ae;
		}
	}
	{
		// f.SetValue(RS.GetFloat(f.rsKeyName));
		FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* L_28 = V_1;
		FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* L_29 = V_1;
		String_t* L_30;
		L_30 = FieldWithRemoteSettingsKey_get_rsKeyName_m19EE15BFF91980A5BADA0AC93848010EDCFDA9CD_inline(L_29, NULL);
		float L_31;
		L_31 = RemoteSettings_GetFloat_m4062B14E0CF358581AC256B668F74BB542777807(L_30, NULL);
		float L_32 = L_31;
		RuntimeObject* L_33 = Box(Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var, &L_32);
		FieldWithRemoteSettingsKey_SetValue_m2CF14A7D7173DEDAEB81D2CA0B4DB87D562FF279(L_28, L_33, NULL);
		goto IL_00fb;
	}

IL_00ae:
	{
		// else if (f.type == "int")
		FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* L_34 = V_1;
		String_t* L_35;
		L_35 = FieldWithRemoteSettingsKey_get_type_mA089AE382B7C2233B18ECCD87E750BBC5CAD98CD_inline(L_34, NULL);
		bool L_36;
		L_36 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_35, _stringLiteral0BA8CB3B900ECEF5E697192B8CDA6B626EB0CE72, NULL);
		if (!L_36)
		{
			goto IL_00d8;
		}
	}
	{
		// f.SetValue(RS.GetInt(f.rsKeyName));
		FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* L_37 = V_1;
		FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* L_38 = V_1;
		String_t* L_39;
		L_39 = FieldWithRemoteSettingsKey_get_rsKeyName_m19EE15BFF91980A5BADA0AC93848010EDCFDA9CD_inline(L_38, NULL);
		int32_t L_40;
		L_40 = RemoteSettings_GetInt_m6B2CCA75F70E2052BC562E2F6C2D91F1CB66335E(L_39, NULL);
		int32_t L_41 = L_40;
		RuntimeObject* L_42 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_41);
		FieldWithRemoteSettingsKey_SetValue_m2CF14A7D7173DEDAEB81D2CA0B4DB87D562FF279(L_37, L_42, NULL);
		goto IL_00fb;
	}

IL_00d8:
	{
		// else if (f.type == "string")
		FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* L_43 = V_1;
		String_t* L_44;
		L_44 = FieldWithRemoteSettingsKey_get_type_mA089AE382B7C2233B18ECCD87E750BBC5CAD98CD_inline(L_43, NULL);
		bool L_45;
		L_45 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_44, _stringLiteral2F1705A1AA8BA6FCE863E7F2CBA4BC28458C77AE, NULL);
		if (!L_45)
		{
			goto IL_00fb;
		}
	}
	{
		// f.SetValue(RS.GetString(f.rsKeyName));
		FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* L_46 = V_1;
		FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* L_47 = V_1;
		String_t* L_48;
		L_48 = FieldWithRemoteSettingsKey_get_rsKeyName_m19EE15BFF91980A5BADA0AC93848010EDCFDA9CD_inline(L_47, NULL);
		String_t* L_49;
		L_49 = RemoteSettings_GetString_mD41BAF75A8DD0F38CD78B4AE26E22D3D594B337C(L_48, NULL);
		FieldWithRemoteSettingsKey_SetValue_m2CF14A7D7173DEDAEB81D2CA0B4DB87D562FF279(L_46, L_49, NULL);
	}

IL_00fb:
	{
		// for (int i = 0; i < m_DriveableProperty.fields.Count; i++)
		int32_t L_50 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_50, 1));
	}

IL_00ff:
	{
		// for (int i = 0; i < m_DriveableProperty.fields.Count; i++)
		int32_t L_51 = V_0;
		DriveableProperty_tC1D2C98D55A7EB8BDC2F9E8F0EBB78888D4AD158* L_52 = __this->___m_DriveableProperty_4;
		List_1_tCD3B17EC7F519BA836765E50183BF8C97D94302A* L_53;
		L_53 = DriveableProperty_get_fields_m9554FDEAB0CCB85D415E193F70395A69E2741CA2_inline(L_52, NULL);
		int32_t L_54;
		L_54 = List_1_get_Count_mF20CB9BE944B8F7DA3107E94690C3A7D29E3CB4A_inline(L_53, List_1_get_Count_mF20CB9BE944B8F7DA3107E94690C3A7D29E3CB4A_RuntimeMethod_var);
		if ((((int32_t)L_51) < ((int32_t)L_54)))
		{
			goto IL_0007;
		}
	}
	{
		// }
		return;
	}
}
// System.Void UnityEngine.Analytics.RemoteSettings::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RemoteSettings__ctor_mA60EEA3BFB8A3D3BDC846261AFA98A4274D3756C (RemoteSettings_tBBDE520F6CF4BC18914F1A4A058CF33ECC003F75* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DriveableProperty_tC1D2C98D55A7EB8BDC2F9E8F0EBB78888D4AD158_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// private DriveableProperty m_DriveableProperty = new DriveableProperty();
		DriveableProperty_tC1D2C98D55A7EB8BDC2F9E8F0EBB78888D4AD158* L_0 = (DriveableProperty_tC1D2C98D55A7EB8BDC2F9E8F0EBB78888D4AD158*)il2cpp_codegen_object_new(DriveableProperty_tC1D2C98D55A7EB8BDC2F9E8F0EBB78888D4AD158_il2cpp_TypeInfo_var);
		DriveableProperty__ctor_mF0E622CFD914ADE0C3DAEAB06A4DCEB71F7A9B69(L_0, NULL);
		__this->___m_DriveableProperty_4 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_DriveableProperty_4), (void*)L_0);
		MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline (String_t* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->____stringLength_4;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR List_1_tCD3B17EC7F519BA836765E50183BF8C97D94302A* DriveableProperty_get_fields_m9554FDEAB0CCB85D415E193F70395A69E2741CA2_inline (DriveableProperty_tC1D2C98D55A7EB8BDC2F9E8F0EBB78888D4AD158* __this, const RuntimeMethod* method) 
{
	{
		// get { return m_Fields; }
		List_1_tCD3B17EC7F519BA836765E50183BF8C97D94302A* L_0 = __this->___m_Fields_0;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* FieldWithRemoteSettingsKey_get_rsKeyName_m19EE15BFF91980A5BADA0AC93848010EDCFDA9CD_inline (FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* __this, const RuntimeMethod* method) 
{
	{
		// get { return m_RSKeyName; }
		String_t* L_0 = __this->___m_RSKeyName_2;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* FieldWithRemoteSettingsKey_get_target_mD39F72EB38D8BAAA991F8CB235B297BC0DDA799F_inline (FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* __this, const RuntimeMethod* method) 
{
	{
		// get { return m_Target; }
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_0 = __this->___m_Target_0;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* FieldWithRemoteSettingsKey_get_fieldPath_m5E82F5C7159D1C9E71CE01C0C10F848C7FC1FABD_inline (FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* __this, const RuntimeMethod* method) 
{
	{
		// get { return m_FieldPath; }
		String_t* L_0 = __this->___m_FieldPath_1;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* FieldWithRemoteSettingsKey_get_type_mA089AE382B7C2233B18ECCD87E750BBC5CAD98CD_inline (FieldWithRemoteSettingsKey_t463854198AD4DFDD3EF1CFAED23EE67D40632121* __this, const RuntimeMethod* method) 
{
	{
		// get { return m_Type; }
		String_t* L_0 = __this->___m_Type_3;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t List_1_get_Count_m4407E4C389F22B8CEC282C15D56516658746C383_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = (int32_t)__this->____size_2;
		return L_0;
	}
}
