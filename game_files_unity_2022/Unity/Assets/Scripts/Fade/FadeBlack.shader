Shader "Hidden/FadeBlack" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _blackFade ("Black Percent", Range (0, 1)) = 0
    }

    SubShader {
        Pass {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            
            #include "UnityCG.cginc"
            
            uniform sampler2D _MainTex;
            uniform float _blackFade;
            
            float4 frag(v2f_img i) : COLOR {
                float4 c = tex2D(_MainTex, i.uv);
                float3 bw = float3 (c.r, c.g, c.b);
                bw.r *= 1 - _blackFade;
                bw.g *= 1 - _blackFade;
                bw.b *= 1 - _blackFade;
                
                float4 result = c;
                result.rgb = lerp(c.rgb, bw, _blackFade);
                return result;
            }
            ENDCG
        }
    }
}