﻿/*Please do support www.bitshiftprogrammer.com by joining the facebook page : fb.com/BitshiftProgrammer
Legal Stuff:
This code is free to use no restrictions but attribution would be appreciated.
Any damage caused either partly or completly due to usage this stuff is not my responsibility*/
Shader "BitshiftProgrammer/Liquid"
{
    Properties
    {
		_Colour ("Colour", Color) = (1,1,1,1)
        _FillAmount ("Fill Amount", Range(-10,10)) = 0.0
		[HideInInspector] _WobbleX ("WobbleX", Range(-1,1)) = 0.0
		[HideInInspector] _WobbleZ ("WobbleZ", Range(-1,1)) = 0.0
    }
 
    SubShader
    {
        Tags {"RenderType"="Opaque" "Queue"="Transparent"  "DisableBatching" = "True" }
  
        Pass
        {
		    Zwrite off
            Blend DstColor OneMinusSrcAlpha
            Cull Off // we want the front and back faces
            AlphaToMask off
         CGPROGRAM
         #pragma vertex vert
         #pragma fragment frag
         #include "UnityCG.cginc"
 
         struct appdata
         {
           float4 vertex : POSITION;
		   float3 normal : NORMAL;
         };
 
         struct v2f
         {
            float4 vertex : SV_POSITION;
			float3 viewDir : COLOR;
		    float3 normal : COLOR2;
			float fillEdge : TEXCOORD2;
         };
 
         float _FillAmount, _WobbleX, _WobbleZ;
         float4 _TopColor, _FoamColor, _Colour;
         float _Rim;
           
		 float4 RotateAroundYInDegrees (float4 vertex, float degrees)
         {
			float alpha = degrees * UNITY_PI / 180;
			float sina, cosa;
			sincos(alpha, sina, cosa);
			float2x2 m = float2x2(cosa, sina, -sina, cosa);
			return float4(vertex.yz , mul(m, vertex.xz)).xzyw ;
         }

         v2f vert (appdata v)
         {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            float3 worldPos = mul (unity_ObjectToWorld, v.vertex.xyz);
			// rotate it around XY
			float3 worldPosX = RotateAroundYInDegrees(float4(worldPos,0),360);
			// rotate around XZ
			float3 worldPosZ = float3 (worldPosX.y, worldPosX.z, worldPosX.x);
			//v.vertex.y+=sin(worldPosZ.x + _Time.w);
			// combine rotations with worldPos, based on sine wave from script
			float3 worldPosAdjusted = worldPos + (worldPosX  * _WobbleX)+ (worldPosZ* _WobbleZ); 
			// how high up the liquid is
			o.fillEdge =  worldPosAdjusted.y + _FillAmount;
			o.viewDir = normalize(ObjSpaceViewDir(v.vertex));
            o.normal = v.normal;
            return o;
         }

         fixed4 frag (v2f i, fixed facing : VFACE) : SV_Target
         {


		   float4 result = step(i.fillEdge, 0.5);
           float4 resultColored = result * _Colour;

		   return resultColored;
         }
         ENDCG
        }
    }
}