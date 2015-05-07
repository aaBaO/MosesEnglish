Shader "EnCard/Water Wiggle" {
Properties {
 _Color ("Main Color", Color) = (1,1,1,1)
 _MainTex ("Base (RGB)", 2D) = "white" {}
 _WiggleTex ("Base (RGB)", 2D) = "white" {}
 _MTex2 ("Base (RGB)", 2D) = "white" {}
 _WiggleStrength ("Wiggle Strength", Range(0.01,0.1)) = 0.01
 _Alphas ("Alpha", Range(-1,0)) = 0
}
SubShader { 
 LOD 200
 Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="True" "RenderType"="Transparent" }
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="ForwardBase" "QUEUE"="Transparent" "IGNOREPROJECTOR"="True" "RenderType"="Transparent" }
  ZWrite Off
  Blend SrcAlpha OneMinusSrcAlpha
  AlphaTest Greater 0
  ColorMask RGB
Program "vp" {
SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_SHC;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;

uniform highp vec4 _WiggleTex_ST;
uniform highp mat4 _Object2World;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 _MTex2_ST;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  highp vec3 shlight_1;
  highp vec4 tmpvar_2;
  lowp vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  tmpvar_2.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_2.zw = ((_glesMultiTexCoord0.xy * _WiggleTex_ST.xy) + _WiggleTex_ST.zw);
  mat3 tmpvar_5;
  tmpvar_5[0] = _Object2World[0].xyz;
  tmpvar_5[1] = _Object2World[1].xyz;
  tmpvar_5[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_6;
  tmpvar_6 = (tmpvar_5 * (normalize(_glesNormal) * unity_Scale.w));
  tmpvar_3 = tmpvar_6;
  highp vec4 tmpvar_7;
  tmpvar_7.w = 1.00000;
  tmpvar_7.xyz = tmpvar_6;
  mediump vec3 tmpvar_8;
  mediump vec4 normal_9;
  normal_9 = tmpvar_7;
  mediump vec3 x3_10;
  highp float vC_11;
  mediump vec3 x2_12;
  mediump vec3 x1_13;
  highp float tmpvar_14;
  tmpvar_14 = dot (unity_SHAr, normal_9);
  x1_13.x = tmpvar_14;
  highp float tmpvar_15;
  tmpvar_15 = dot (unity_SHAg, normal_9);
  x1_13.y = tmpvar_15;
  highp float tmpvar_16;
  tmpvar_16 = dot (unity_SHAb, normal_9);
  x1_13.z = tmpvar_16;
  mediump vec4 tmpvar_17;
  tmpvar_17 = (normal_9.xyzz * normal_9.yzzx);
  highp float tmpvar_18;
  tmpvar_18 = dot (unity_SHBr, tmpvar_17);
  x2_12.x = tmpvar_18;
  highp float tmpvar_19;
  tmpvar_19 = dot (unity_SHBg, tmpvar_17);
  x2_12.y = tmpvar_19;
  highp float tmpvar_20;
  tmpvar_20 = dot (unity_SHBb, tmpvar_17);
  x2_12.z = tmpvar_20;
  mediump float tmpvar_21;
  tmpvar_21 = ((normal_9.x * normal_9.x) - (normal_9.y * normal_9.y));
  vC_11 = tmpvar_21;
  highp vec3 tmpvar_22;
  tmpvar_22 = (unity_SHC.xyz * vC_11);
  x3_10 = tmpvar_22;
  tmpvar_8 = ((x1_13 + x2_12) + x3_10);
  shlight_1 = tmpvar_8;
  tmpvar_4 = shlight_1;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_2;
  xlv_TEXCOORD1 = ((_glesMultiTexCoord0.xy * _MTex2_ST.xy) + _MTex2_ST.zw);
  xlv_TEXCOORD2 = tmpvar_3;
  xlv_TEXCOORD3 = tmpvar_4;
}



#endif
#ifdef FRAGMENT

varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform lowp vec4 _WorldSpaceLightPos0;
uniform sampler2D _WiggleTex;
uniform highp float _WiggleStrength;
uniform highp vec4 _SinTime;
uniform sampler2D _MainTex;
uniform sampler2D _MTex2;
uniform lowp vec4 _LightColor0;
uniform highp vec4 _CosTime;
uniform lowp vec4 _Color;
uniform highp float _Alphas;
void main ()
{
  lowp vec4 c_1;
  highp vec2 tmpvar_2;
  lowp float tmpvar_3;
  highp vec2 tc2_4;
  tc2_4.x = (xlv_TEXCOORD0.z - _SinTime.x);
  tc2_4.y = (xlv_TEXCOORD0.w + _CosTime.x);
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_WiggleTex, tc2_4);
  tmpvar_2.x = (xlv_TEXCOORD0.x - (tmpvar_5.x * _WiggleStrength));
  tmpvar_2.y = (xlv_TEXCOORD0.y + ((tmpvar_5.z * _WiggleStrength) * 1.50000));
  lowp vec4 tmpvar_6;
  tmpvar_6 = (texture2D (_MainTex, tmpvar_2) * _Color);
  lowp vec4 tmpvar_7;
  tmpvar_7 = (texture2D (_MTex2, xlv_TEXCOORD1) * _Color);
  highp float tmpvar_8;
  tmpvar_8 = (tmpvar_7.w + _Alphas);
  tmpvar_3 = tmpvar_8;
  lowp vec4 c_9;
  c_9.xyz = ((tmpvar_6.xyz * _LightColor0.xyz) * (max (0.000000, dot (xlv_TEXCOORD2, _WorldSpaceLightPos0.xyz)) * 2.00000));
  c_9.w = tmpvar_3;
  c_1.xyz = (c_9.xyz + (tmpvar_6.xyz * xlv_TEXCOORD3));
  c_1.w = tmpvar_3;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec2 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_LightmapST;

uniform highp vec4 _WiggleTex_ST;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 _MTex2_ST;
attribute vec4 _glesMultiTexCoord1;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesVertex;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.zw = ((_glesMultiTexCoord0.xy * _WiggleTex_ST.xy) + _WiggleTex_ST.zw);
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = ((_glesMultiTexCoord0.xy * _MTex2_ST.xy) + _MTex2_ST.zw);
  xlv_TEXCOORD2 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
}



#endif
#ifdef FRAGMENT

varying highp vec2 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D unity_Lightmap;
uniform sampler2D _WiggleTex;
uniform highp float _WiggleStrength;
uniform highp vec4 _SinTime;
uniform sampler2D _MainTex;
uniform sampler2D _MTex2;
uniform highp vec4 _CosTime;
uniform lowp vec4 _Color;
uniform highp float _Alphas;
void main ()
{
  lowp vec4 c_1;
  highp vec2 tmpvar_2;
  lowp float tmpvar_3;
  highp vec2 tc2_4;
  tc2_4.x = (xlv_TEXCOORD0.z - _SinTime.x);
  tc2_4.y = (xlv_TEXCOORD0.w + _CosTime.x);
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_WiggleTex, tc2_4);
  tmpvar_2.x = (xlv_TEXCOORD0.x - (tmpvar_5.x * _WiggleStrength));
  tmpvar_2.y = (xlv_TEXCOORD0.y + ((tmpvar_5.z * _WiggleStrength) * 1.50000));
  lowp vec4 tmpvar_6;
  tmpvar_6 = (texture2D (_MTex2, xlv_TEXCOORD1) * _Color);
  highp float tmpvar_7;
  tmpvar_7 = (tmpvar_6.w + _Alphas);
  tmpvar_3 = tmpvar_7;
  c_1.xyz = ((texture2D (_MainTex, tmpvar_2) * _Color).xyz * (2.00000 * texture2D (unity_Lightmap, xlv_TEXCOORD2).xyz));
  c_1.w = tmpvar_3;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec2 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_LightmapST;

uniform highp vec4 _WiggleTex_ST;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 _MTex2_ST;
attribute vec4 _glesMultiTexCoord1;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesVertex;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.zw = ((_glesMultiTexCoord0.xy * _WiggleTex_ST.xy) + _WiggleTex_ST.zw);
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = ((_glesMultiTexCoord0.xy * _MTex2_ST.xy) + _MTex2_ST.zw);
  xlv_TEXCOORD2 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
}



#endif
#ifdef FRAGMENT

varying highp vec2 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D unity_Lightmap;
uniform sampler2D _WiggleTex;
uniform highp float _WiggleStrength;
uniform highp vec4 _SinTime;
uniform sampler2D _MainTex;
uniform sampler2D _MTex2;
uniform highp vec4 _CosTime;
uniform lowp vec4 _Color;
uniform highp float _Alphas;
void main ()
{
  lowp vec4 c_1;
  highp vec2 tmpvar_2;
  lowp float tmpvar_3;
  highp vec2 tc2_4;
  tc2_4.x = (xlv_TEXCOORD0.z - _SinTime.x);
  tc2_4.y = (xlv_TEXCOORD0.w + _CosTime.x);
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_WiggleTex, tc2_4);
  tmpvar_2.x = (xlv_TEXCOORD0.x - (tmpvar_5.x * _WiggleStrength));
  tmpvar_2.y = (xlv_TEXCOORD0.y + ((tmpvar_5.z * _WiggleStrength) * 1.50000));
  lowp vec4 tmpvar_6;
  tmpvar_6 = (texture2D (_MainTex, tmpvar_2) * _Color);
  lowp vec4 tmpvar_7;
  tmpvar_7 = (texture2D (_MTex2, xlv_TEXCOORD1) * _Color);
  highp float tmpvar_8;
  tmpvar_8 = (tmpvar_7.w + _Alphas);
  tmpvar_3 = tmpvar_8;
  mediump vec3 lm_9;
  lowp vec3 tmpvar_10;
  tmpvar_10 = (2.00000 * texture2D (unity_Lightmap, xlv_TEXCOORD2).xyz);
  lm_9 = tmpvar_10;
  mediump vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_6.xyz * lm_9);
  c_1.xyz = tmpvar_11;
  c_1.w = tmpvar_3;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_SHC;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_4LightPosZ0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightAtten0;

uniform highp vec4 _WiggleTex_ST;
uniform highp mat4 _Object2World;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 _MTex2_ST;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  highp vec3 shlight_1;
  highp vec4 tmpvar_2;
  lowp vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  tmpvar_2.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_2.zw = ((_glesMultiTexCoord0.xy * _WiggleTex_ST.xy) + _WiggleTex_ST.zw);
  mat3 tmpvar_5;
  tmpvar_5[0] = _Object2World[0].xyz;
  tmpvar_5[1] = _Object2World[1].xyz;
  tmpvar_5[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_6;
  tmpvar_6 = (tmpvar_5 * (normalize(_glesNormal) * unity_Scale.w));
  tmpvar_3 = tmpvar_6;
  highp vec4 tmpvar_7;
  tmpvar_7.w = 1.00000;
  tmpvar_7.xyz = tmpvar_6;
  mediump vec3 tmpvar_8;
  mediump vec4 normal_9;
  normal_9 = tmpvar_7;
  mediump vec3 x3_10;
  highp float vC_11;
  mediump vec3 x2_12;
  mediump vec3 x1_13;
  highp float tmpvar_14;
  tmpvar_14 = dot (unity_SHAr, normal_9);
  x1_13.x = tmpvar_14;
  highp float tmpvar_15;
  tmpvar_15 = dot (unity_SHAg, normal_9);
  x1_13.y = tmpvar_15;
  highp float tmpvar_16;
  tmpvar_16 = dot (unity_SHAb, normal_9);
  x1_13.z = tmpvar_16;
  mediump vec4 tmpvar_17;
  tmpvar_17 = (normal_9.xyzz * normal_9.yzzx);
  highp float tmpvar_18;
  tmpvar_18 = dot (unity_SHBr, tmpvar_17);
  x2_12.x = tmpvar_18;
  highp float tmpvar_19;
  tmpvar_19 = dot (unity_SHBg, tmpvar_17);
  x2_12.y = tmpvar_19;
  highp float tmpvar_20;
  tmpvar_20 = dot (unity_SHBb, tmpvar_17);
  x2_12.z = tmpvar_20;
  mediump float tmpvar_21;
  tmpvar_21 = ((normal_9.x * normal_9.x) - (normal_9.y * normal_9.y));
  vC_11 = tmpvar_21;
  highp vec3 tmpvar_22;
  tmpvar_22 = (unity_SHC.xyz * vC_11);
  x3_10 = tmpvar_22;
  tmpvar_8 = ((x1_13 + x2_12) + x3_10);
  shlight_1 = tmpvar_8;
  tmpvar_4 = shlight_1;
  highp vec3 tmpvar_23;
  tmpvar_23 = (_Object2World * _glesVertex).xyz;
  highp vec4 tmpvar_24;
  tmpvar_24 = (unity_4LightPosX0 - tmpvar_23.x);
  highp vec4 tmpvar_25;
  tmpvar_25 = (unity_4LightPosY0 - tmpvar_23.y);
  highp vec4 tmpvar_26;
  tmpvar_26 = (unity_4LightPosZ0 - tmpvar_23.z);
  highp vec4 tmpvar_27;
  tmpvar_27 = (((tmpvar_24 * tmpvar_24) + (tmpvar_25 * tmpvar_25)) + (tmpvar_26 * tmpvar_26));
  highp vec4 tmpvar_28;
  tmpvar_28 = (max (vec4(0.000000, 0.000000, 0.000000, 0.000000), ((((tmpvar_24 * tmpvar_6.x) + (tmpvar_25 * tmpvar_6.y)) + (tmpvar_26 * tmpvar_6.z)) * inversesqrt(tmpvar_27))) * (1.0/((1.00000 + (tmpvar_27 * unity_4LightAtten0)))));
  highp vec3 tmpvar_29;
  tmpvar_29 = (tmpvar_4 + ((((unity_LightColor[0].xyz * tmpvar_28.x) + (unity_LightColor[1].xyz * tmpvar_28.y)) + (unity_LightColor[2].xyz * tmpvar_28.z)) + (unity_LightColor[3].xyz * tmpvar_28.w)));
  tmpvar_4 = tmpvar_29;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_2;
  xlv_TEXCOORD1 = ((_glesMultiTexCoord0.xy * _MTex2_ST.xy) + _MTex2_ST.zw);
  xlv_TEXCOORD2 = tmpvar_3;
  xlv_TEXCOORD3 = tmpvar_4;
}



#endif
#ifdef FRAGMENT

varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform lowp vec4 _WorldSpaceLightPos0;
uniform sampler2D _WiggleTex;
uniform highp float _WiggleStrength;
uniform highp vec4 _SinTime;
uniform sampler2D _MainTex;
uniform sampler2D _MTex2;
uniform lowp vec4 _LightColor0;
uniform highp vec4 _CosTime;
uniform lowp vec4 _Color;
uniform highp float _Alphas;
void main ()
{
  lowp vec4 c_1;
  highp vec2 tmpvar_2;
  lowp float tmpvar_3;
  highp vec2 tc2_4;
  tc2_4.x = (xlv_TEXCOORD0.z - _SinTime.x);
  tc2_4.y = (xlv_TEXCOORD0.w + _CosTime.x);
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_WiggleTex, tc2_4);
  tmpvar_2.x = (xlv_TEXCOORD0.x - (tmpvar_5.x * _WiggleStrength));
  tmpvar_2.y = (xlv_TEXCOORD0.y + ((tmpvar_5.z * _WiggleStrength) * 1.50000));
  lowp vec4 tmpvar_6;
  tmpvar_6 = (texture2D (_MainTex, tmpvar_2) * _Color);
  lowp vec4 tmpvar_7;
  tmpvar_7 = (texture2D (_MTex2, xlv_TEXCOORD1) * _Color);
  highp float tmpvar_8;
  tmpvar_8 = (tmpvar_7.w + _Alphas);
  tmpvar_3 = tmpvar_8;
  lowp vec4 c_9;
  c_9.xyz = ((tmpvar_6.xyz * _LightColor0.xyz) * (max (0.000000, dot (xlv_TEXCOORD2, _WorldSpaceLightPos0.xyz)) * 2.00000));
  c_9.w = tmpvar_3;
  c_1.xyz = (c_9.xyz + (tmpvar_6.xyz * xlv_TEXCOORD3));
  c_1.w = tmpvar_3;
  gl_FragData[0] = c_1;
}



#endif"
}
}
Program "fp" {
SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
"!!GLES"
}
}
 }
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="ForwardAdd" "QUEUE"="Transparent" "IGNOREPROJECTOR"="True" "RenderType"="Transparent" }
  ZWrite Off
  Fog {
   Color (0,0,0,0)
  }
  Blend SrcAlpha One
  AlphaTest Greater 0
  ColorMask RGB
Program "vp" {
SubProgram "gles " {
Keywords { "POINT" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec3 xlv_TEXCOORD4;
varying mediump vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;

uniform highp vec4 _WorldSpaceLightPos0;
uniform highp vec4 _WiggleTex_ST;
uniform highp mat4 _Object2World;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 _MTex2_ST;
uniform highp mat4 _LightMatrix0;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  highp vec4 tmpvar_1;
  lowp vec3 tmpvar_2;
  mediump vec3 tmpvar_3;
  tmpvar_1.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.zw = ((_glesMultiTexCoord0.xy * _WiggleTex_ST.xy) + _WiggleTex_ST.zw);
  mat3 tmpvar_4;
  tmpvar_4[0] = _Object2World[0].xyz;
  tmpvar_4[1] = _Object2World[1].xyz;
  tmpvar_4[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_5;
  tmpvar_5 = (tmpvar_4 * (normalize(_glesNormal) * unity_Scale.w));
  tmpvar_2 = tmpvar_5;
  highp vec3 tmpvar_6;
  tmpvar_6 = (_WorldSpaceLightPos0.xyz - (_Object2World * _glesVertex).xyz);
  tmpvar_3 = tmpvar_6;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = ((_glesMultiTexCoord0.xy * _MTex2_ST.xy) + _MTex2_ST.zw);
  xlv_TEXCOORD2 = tmpvar_2;
  xlv_TEXCOORD3 = tmpvar_3;
  xlv_TEXCOORD4 = (_LightMatrix0 * (_Object2World * _glesVertex)).xyz;
}



#endif
#ifdef FRAGMENT

varying highp vec3 xlv_TEXCOORD4;
varying mediump vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D _WiggleTex;
uniform highp float _WiggleStrength;
uniform highp vec4 _SinTime;
uniform sampler2D _MainTex;
uniform sampler2D _MTex2;
uniform sampler2D _LightTexture0;
uniform lowp vec4 _LightColor0;
uniform highp vec4 _CosTime;
uniform lowp vec4 _Color;
uniform highp float _Alphas;
void main ()
{
  lowp vec4 c_1;
  lowp vec3 lightDir_2;
  highp vec2 tmpvar_3;
  lowp float tmpvar_4;
  highp vec2 tc2_5;
  tc2_5.x = (xlv_TEXCOORD0.z - _SinTime.x);
  tc2_5.y = (xlv_TEXCOORD0.w + _CosTime.x);
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_WiggleTex, tc2_5);
  tmpvar_3.x = (xlv_TEXCOORD0.x - (tmpvar_6.x * _WiggleStrength));
  tmpvar_3.y = (xlv_TEXCOORD0.y + ((tmpvar_6.z * _WiggleStrength) * 1.50000));
  lowp vec4 tmpvar_7;
  tmpvar_7 = (texture2D (_MTex2, xlv_TEXCOORD1) * _Color);
  highp float tmpvar_8;
  tmpvar_8 = (tmpvar_7.w + _Alphas);
  tmpvar_4 = tmpvar_8;
  mediump vec3 tmpvar_9;
  tmpvar_9 = normalize(xlv_TEXCOORD3);
  lightDir_2 = tmpvar_9;
  highp float tmpvar_10;
  tmpvar_10 = dot (xlv_TEXCOORD4, xlv_TEXCOORD4);
  lowp vec4 c_11;
  c_11.xyz = (((texture2D (_MainTex, tmpvar_3) * _Color).xyz * _LightColor0.xyz) * ((max (0.000000, dot (xlv_TEXCOORD2, lightDir_2)) * texture2D (_LightTexture0, vec2(tmpvar_10)).w) * 2.00000));
  c_11.w = tmpvar_4;
  c_1.xyz = c_11.xyz;
  c_1.w = tmpvar_4;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying mediump vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec4 _WiggleTex_ST;
uniform highp mat4 _Object2World;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 _MTex2_ST;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  highp vec4 tmpvar_1;
  lowp vec3 tmpvar_2;
  mediump vec3 tmpvar_3;
  tmpvar_1.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.zw = ((_glesMultiTexCoord0.xy * _WiggleTex_ST.xy) + _WiggleTex_ST.zw);
  mat3 tmpvar_4;
  tmpvar_4[0] = _Object2World[0].xyz;
  tmpvar_4[1] = _Object2World[1].xyz;
  tmpvar_4[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_5;
  tmpvar_5 = (tmpvar_4 * (normalize(_glesNormal) * unity_Scale.w));
  tmpvar_2 = tmpvar_5;
  highp vec3 tmpvar_6;
  tmpvar_6 = _WorldSpaceLightPos0.xyz;
  tmpvar_3 = tmpvar_6;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = ((_glesMultiTexCoord0.xy * _MTex2_ST.xy) + _MTex2_ST.zw);
  xlv_TEXCOORD2 = tmpvar_2;
  xlv_TEXCOORD3 = tmpvar_3;
}



#endif
#ifdef FRAGMENT

varying mediump vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D _WiggleTex;
uniform highp float _WiggleStrength;
uniform highp vec4 _SinTime;
uniform sampler2D _MainTex;
uniform sampler2D _MTex2;
uniform lowp vec4 _LightColor0;
uniform highp vec4 _CosTime;
uniform lowp vec4 _Color;
uniform highp float _Alphas;
void main ()
{
  lowp vec4 c_1;
  lowp vec3 lightDir_2;
  highp vec2 tmpvar_3;
  lowp float tmpvar_4;
  highp vec2 tc2_5;
  tc2_5.x = (xlv_TEXCOORD0.z - _SinTime.x);
  tc2_5.y = (xlv_TEXCOORD0.w + _CosTime.x);
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_WiggleTex, tc2_5);
  tmpvar_3.x = (xlv_TEXCOORD0.x - (tmpvar_6.x * _WiggleStrength));
  tmpvar_3.y = (xlv_TEXCOORD0.y + ((tmpvar_6.z * _WiggleStrength) * 1.50000));
  lowp vec4 tmpvar_7;
  tmpvar_7 = (texture2D (_MTex2, xlv_TEXCOORD1) * _Color);
  highp float tmpvar_8;
  tmpvar_8 = (tmpvar_7.w + _Alphas);
  tmpvar_4 = tmpvar_8;
  lightDir_2 = xlv_TEXCOORD3;
  lowp vec4 c_9;
  c_9.xyz = (((texture2D (_MainTex, tmpvar_3) * _Color).xyz * _LightColor0.xyz) * (max (0.000000, dot (xlv_TEXCOORD2, lightDir_2)) * 2.00000));
  c_9.w = tmpvar_4;
  c_1.xyz = c_9.xyz;
  c_1.w = tmpvar_4;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "SPOT" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD4;
varying mediump vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;

uniform highp vec4 _WorldSpaceLightPos0;
uniform highp vec4 _WiggleTex_ST;
uniform highp mat4 _Object2World;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 _MTex2_ST;
uniform highp mat4 _LightMatrix0;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  highp vec4 tmpvar_1;
  lowp vec3 tmpvar_2;
  mediump vec3 tmpvar_3;
  tmpvar_1.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.zw = ((_glesMultiTexCoord0.xy * _WiggleTex_ST.xy) + _WiggleTex_ST.zw);
  mat3 tmpvar_4;
  tmpvar_4[0] = _Object2World[0].xyz;
  tmpvar_4[1] = _Object2World[1].xyz;
  tmpvar_4[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_5;
  tmpvar_5 = (tmpvar_4 * (normalize(_glesNormal) * unity_Scale.w));
  tmpvar_2 = tmpvar_5;
  highp vec3 tmpvar_6;
  tmpvar_6 = (_WorldSpaceLightPos0.xyz - (_Object2World * _glesVertex).xyz);
  tmpvar_3 = tmpvar_6;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = ((_glesMultiTexCoord0.xy * _MTex2_ST.xy) + _MTex2_ST.zw);
  xlv_TEXCOORD2 = tmpvar_2;
  xlv_TEXCOORD3 = tmpvar_3;
  xlv_TEXCOORD4 = (_LightMatrix0 * (_Object2World * _glesVertex));
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD4;
varying mediump vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D _WiggleTex;
uniform highp float _WiggleStrength;
uniform highp vec4 _SinTime;
uniform sampler2D _MainTex;
uniform sampler2D _MTex2;
uniform sampler2D _LightTextureB0;
uniform sampler2D _LightTexture0;
uniform lowp vec4 _LightColor0;
uniform highp vec4 _CosTime;
uniform lowp vec4 _Color;
uniform highp float _Alphas;
void main ()
{
  lowp vec4 c_1;
  lowp vec3 lightDir_2;
  highp vec2 tmpvar_3;
  lowp float tmpvar_4;
  highp vec2 tc2_5;
  tc2_5.x = (xlv_TEXCOORD0.z - _SinTime.x);
  tc2_5.y = (xlv_TEXCOORD0.w + _CosTime.x);
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_WiggleTex, tc2_5);
  tmpvar_3.x = (xlv_TEXCOORD0.x - (tmpvar_6.x * _WiggleStrength));
  tmpvar_3.y = (xlv_TEXCOORD0.y + ((tmpvar_6.z * _WiggleStrength) * 1.50000));
  lowp vec4 tmpvar_7;
  tmpvar_7 = (texture2D (_MTex2, xlv_TEXCOORD1) * _Color);
  highp float tmpvar_8;
  tmpvar_8 = (tmpvar_7.w + _Alphas);
  tmpvar_4 = tmpvar_8;
  mediump vec3 tmpvar_9;
  tmpvar_9 = normalize(xlv_TEXCOORD3);
  lightDir_2 = tmpvar_9;
  highp vec2 P_10;
  P_10 = ((xlv_TEXCOORD4.xy / xlv_TEXCOORD4.w) + 0.500000);
  highp float tmpvar_11;
  tmpvar_11 = dot (xlv_TEXCOORD4.xyz, xlv_TEXCOORD4.xyz);
  lowp float atten_12;
  atten_12 = ((float((xlv_TEXCOORD4.z > 0.000000)) * texture2D (_LightTexture0, P_10).w) * texture2D (_LightTextureB0, vec2(tmpvar_11)).w);
  lowp vec4 c_13;
  c_13.xyz = (((texture2D (_MainTex, tmpvar_3) * _Color).xyz * _LightColor0.xyz) * ((max (0.000000, dot (xlv_TEXCOORD2, lightDir_2)) * atten_12) * 2.00000));
  c_13.w = tmpvar_4;
  c_1.xyz = c_13.xyz;
  c_1.w = tmpvar_4;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "POINT_COOKIE" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec3 xlv_TEXCOORD4;
varying mediump vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;

uniform highp vec4 _WorldSpaceLightPos0;
uniform highp vec4 _WiggleTex_ST;
uniform highp mat4 _Object2World;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 _MTex2_ST;
uniform highp mat4 _LightMatrix0;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  highp vec4 tmpvar_1;
  lowp vec3 tmpvar_2;
  mediump vec3 tmpvar_3;
  tmpvar_1.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.zw = ((_glesMultiTexCoord0.xy * _WiggleTex_ST.xy) + _WiggleTex_ST.zw);
  mat3 tmpvar_4;
  tmpvar_4[0] = _Object2World[0].xyz;
  tmpvar_4[1] = _Object2World[1].xyz;
  tmpvar_4[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_5;
  tmpvar_5 = (tmpvar_4 * (normalize(_glesNormal) * unity_Scale.w));
  tmpvar_2 = tmpvar_5;
  highp vec3 tmpvar_6;
  tmpvar_6 = (_WorldSpaceLightPos0.xyz - (_Object2World * _glesVertex).xyz);
  tmpvar_3 = tmpvar_6;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = ((_glesMultiTexCoord0.xy * _MTex2_ST.xy) + _MTex2_ST.zw);
  xlv_TEXCOORD2 = tmpvar_2;
  xlv_TEXCOORD3 = tmpvar_3;
  xlv_TEXCOORD4 = (_LightMatrix0 * (_Object2World * _glesVertex)).xyz;
}



#endif
#ifdef FRAGMENT

varying highp vec3 xlv_TEXCOORD4;
varying mediump vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D _WiggleTex;
uniform highp float _WiggleStrength;
uniform highp vec4 _SinTime;
uniform sampler2D _MainTex;
uniform sampler2D _MTex2;
uniform sampler2D _LightTextureB0;
uniform samplerCube _LightTexture0;
uniform lowp vec4 _LightColor0;
uniform highp vec4 _CosTime;
uniform lowp vec4 _Color;
uniform highp float _Alphas;
void main ()
{
  lowp vec4 c_1;
  lowp vec3 lightDir_2;
  highp vec2 tmpvar_3;
  lowp float tmpvar_4;
  highp vec2 tc2_5;
  tc2_5.x = (xlv_TEXCOORD0.z - _SinTime.x);
  tc2_5.y = (xlv_TEXCOORD0.w + _CosTime.x);
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_WiggleTex, tc2_5);
  tmpvar_3.x = (xlv_TEXCOORD0.x - (tmpvar_6.x * _WiggleStrength));
  tmpvar_3.y = (xlv_TEXCOORD0.y + ((tmpvar_6.z * _WiggleStrength) * 1.50000));
  lowp vec4 tmpvar_7;
  tmpvar_7 = (texture2D (_MTex2, xlv_TEXCOORD1) * _Color);
  highp float tmpvar_8;
  tmpvar_8 = (tmpvar_7.w + _Alphas);
  tmpvar_4 = tmpvar_8;
  mediump vec3 tmpvar_9;
  tmpvar_9 = normalize(xlv_TEXCOORD3);
  lightDir_2 = tmpvar_9;
  highp float tmpvar_10;
  tmpvar_10 = dot (xlv_TEXCOORD4, xlv_TEXCOORD4);
  lowp vec4 c_11;
  c_11.xyz = (((texture2D (_MainTex, tmpvar_3) * _Color).xyz * _LightColor0.xyz) * ((max (0.000000, dot (xlv_TEXCOORD2, lightDir_2)) * (texture2D (_LightTextureB0, vec2(tmpvar_10)).w * textureCube (_LightTexture0, xlv_TEXCOORD4).w)) * 2.00000));
  c_11.w = tmpvar_4;
  c_1.xyz = c_11.xyz;
  c_1.w = tmpvar_4;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL_COOKIE" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec2 xlv_TEXCOORD4;
varying mediump vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec4 _WiggleTex_ST;
uniform highp mat4 _Object2World;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 _MTex2_ST;
uniform highp mat4 _LightMatrix0;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  highp vec4 tmpvar_1;
  lowp vec3 tmpvar_2;
  mediump vec3 tmpvar_3;
  tmpvar_1.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.zw = ((_glesMultiTexCoord0.xy * _WiggleTex_ST.xy) + _WiggleTex_ST.zw);
  mat3 tmpvar_4;
  tmpvar_4[0] = _Object2World[0].xyz;
  tmpvar_4[1] = _Object2World[1].xyz;
  tmpvar_4[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_5;
  tmpvar_5 = (tmpvar_4 * (normalize(_glesNormal) * unity_Scale.w));
  tmpvar_2 = tmpvar_5;
  highp vec3 tmpvar_6;
  tmpvar_6 = _WorldSpaceLightPos0.xyz;
  tmpvar_3 = tmpvar_6;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = ((_glesMultiTexCoord0.xy * _MTex2_ST.xy) + _MTex2_ST.zw);
  xlv_TEXCOORD2 = tmpvar_2;
  xlv_TEXCOORD3 = tmpvar_3;
  xlv_TEXCOORD4 = (_LightMatrix0 * (_Object2World * _glesVertex)).xy;
}



#endif
#ifdef FRAGMENT

varying highp vec2 xlv_TEXCOORD4;
varying mediump vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D _WiggleTex;
uniform highp float _WiggleStrength;
uniform highp vec4 _SinTime;
uniform sampler2D _MainTex;
uniform sampler2D _MTex2;
uniform sampler2D _LightTexture0;
uniform lowp vec4 _LightColor0;
uniform highp vec4 _CosTime;
uniform lowp vec4 _Color;
uniform highp float _Alphas;
void main ()
{
  lowp vec4 c_1;
  lowp vec3 lightDir_2;
  highp vec2 tmpvar_3;
  lowp float tmpvar_4;
  highp vec2 tc2_5;
  tc2_5.x = (xlv_TEXCOORD0.z - _SinTime.x);
  tc2_5.y = (xlv_TEXCOORD0.w + _CosTime.x);
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_WiggleTex, tc2_5);
  tmpvar_3.x = (xlv_TEXCOORD0.x - (tmpvar_6.x * _WiggleStrength));
  tmpvar_3.y = (xlv_TEXCOORD0.y + ((tmpvar_6.z * _WiggleStrength) * 1.50000));
  lowp vec4 tmpvar_7;
  tmpvar_7 = (texture2D (_MTex2, xlv_TEXCOORD1) * _Color);
  highp float tmpvar_8;
  tmpvar_8 = (tmpvar_7.w + _Alphas);
  tmpvar_4 = tmpvar_8;
  lightDir_2 = xlv_TEXCOORD3;
  lowp vec4 c_9;
  c_9.xyz = (((texture2D (_MainTex, tmpvar_3) * _Color).xyz * _LightColor0.xyz) * ((max (0.000000, dot (xlv_TEXCOORD2, lightDir_2)) * texture2D (_LightTexture0, xlv_TEXCOORD4).w) * 2.00000));
  c_9.w = tmpvar_4;
  c_1.xyz = c_9.xyz;
  c_1.w = tmpvar_4;
  gl_FragData[0] = c_1;
}



#endif"
}
}
Program "fp" {
SubProgram "gles " {
Keywords { "POINT" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "SPOT" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "POINT_COOKIE" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL_COOKIE" }
"!!GLES"
}
}
 }
}
Fallback "VertexLit"
}