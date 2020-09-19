Shader "Custom/AlwaysDisplay" {
Properties {
_MainTex ("Base (RGB)", 2D) = "white" {}
}
SubShader {
Tags { "RenderType"="Transparent"  "Queue" = "Geometry+1"}
LOD 200
Pass{
Color (1,0,0,1)
ZTest Always
ZWrite Off
SetTexture[_MainTex] {
//constantColor (0,0,1,1)
//combine constant * texture 
}
}
Pass{
Material{
Diffuse (1,1,1,1)
                Ambient (1,1,1,1)
            }
            
Lighting On
Color(1,0,0,1)
SetTexture [_MainTex]{Combine texture * primary DOUBLE}
}
} 
FallBack "Diffuse"
}