using UnityEngine;
using UnityEditor;
using System.Collections;

public class ShaderMaterialsEditor : ShaderGUI
{
    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        bool bEnableCutOut = false;
        bool bEnableUVRotation = false;
        bool bEnableUVScroll = false;
		bool bEnableUVMirror = false;
		bool bEnableBloom = false;
        Material targetMat = materialEditor.target as Material;
        foreach (MaterialProperty property in properties)
        {
            if (property.type == MaterialProperty.PropType.Texture)
            {
                if (property.name.Equals(EffectShaderPropertyStr.CutTexStr))
                {
                    if (property.textureValue != null)
                    {
                        bEnableCutOut = true;
                    }
                }
                materialEditor.TextureProperty(property, GetPropertyChineseName(property.name));
            }
            else if (property.type == MaterialProperty.PropType.Color)
            {
                materialEditor.ColorProperty(property, GetPropertyChineseName(property.name));
            }
            else if (property.type == MaterialProperty.PropType.Range)
            {
                materialEditor.RangeProperty(property, GetPropertyChineseName(property.name));
				if (property.name.Equals(EffectShaderPropertyStr.UVMirrorX)
					&& property.floatValue != 0.0f)
				{
					bEnableUVMirror = true;
				}
				else if (property.name.Equals(EffectShaderPropertyStr.UVMirrorY)
					&& property.floatValue != 0.0f)
				{
					bEnableUVMirror = true;
				}
				else if(property.name.Equals(EffectShaderPropertyStr.EmissionGain)
					&& property.floatValue != 0.0f)
				{
					bEnableBloom = true;
				}
            }
            else if (property.type == MaterialProperty.PropType.Float)
            {
                if (property.name.Equals(EffectShaderPropertyStr.MainRotationStr)
                    && property.floatValue != 0.0f)
                {
                    bEnableUVRotation = true;
                }
                else if (property.name.Equals(EffectShaderPropertyStr.UVScrollX)
                    && property.floatValue != 0.0f)
                {
                    bEnableUVScroll = true;
                }
                else if (property.name.Equals(EffectShaderPropertyStr.UVScrollY)
                    && property.floatValue != 0.0f)
                {
                    bEnableUVScroll = true;
                }
                if (bEnableCutOut)
                {
                    if (property.name.Equals(EffectShaderPropertyStr.CutRotationStr)
                        && property.floatValue != 0.0f)
                    {
                        bEnableUVRotation = true;
                    }
                    else if (property.name.Equals(EffectShaderPropertyStr.UVCutScrollX)
                        && property.floatValue != 0.0f)
                    {
                        bEnableUVScroll = true;
                    }
                    else if (property.name.Equals(EffectShaderPropertyStr.UVCutScrollY)
                        && property.floatValue != 0.0f)
                    {
                        bEnableUVScroll = true;
                    }
                }

                materialEditor.FloatProperty(property, GetPropertyChineseName(property.name));
            }
        }
        if (bEnableCutOut)
        {
            targetMat.EnableKeyword(EffectShaderPropertyStr.EnableAlphaMaskStr);
        }
        else
        {
            targetMat.DisableKeyword(EffectShaderPropertyStr.EnableAlphaMaskStr);
        }
        if (bEnableUVRotation)
        {
            targetMat.EnableKeyword(EffectShaderPropertyStr.EnableUVRotationStr);
        }
        else
        {
            targetMat.DisableKeyword(EffectShaderPropertyStr.EnableUVRotationStr);
        }
        if (bEnableUVScroll)
        {
            targetMat.EnableKeyword(EffectShaderPropertyStr.EnableUVScrollStr);
        }
        else
        {
            targetMat.DisableKeyword(EffectShaderPropertyStr.EnableUVScrollStr);
        }
		if (bEnableUVMirror)        
		{
			targetMat.EnableKeyword(EffectShaderPropertyStr.EnableUVMirror);
		}
		else
		{
			targetMat.DisableKeyword(EffectShaderPropertyStr.EnableUVMirror);
		}
		if (bEnableBloom) {
			targetMat.EnableKeyword(EffectShaderPropertyStr.EnableBloom);
		} 
		else {
			targetMat.DisableKeyword(EffectShaderPropertyStr.EnableBloom);
		}
    }

    string GetPropertyChineseName(string propertyName)
    {
		if (propertyName.Equals (EffectShaderPropertyStr.MainTexStr)) {
			return "Main Texture";
		} else if (propertyName.Equals (EffectShaderPropertyStr.CutTexStr)) {
			return "CutOut Texture";
		} else if (propertyName.Equals (EffectShaderPropertyStr.ColorStr)) {
			return "Color";
		} else if (propertyName.Equals (EffectShaderPropertyStr.CutOffStr)) {
			return "CutOut Alpha";
		} else if (propertyName.Equals (EffectShaderPropertyStr.MainRotationStr)) {
			return "Main Texture UV Rotation";
		} else if (propertyName.Equals (EffectShaderPropertyStr.CutRotationStr)) {
			return "CutOut UV Rotation";
		} else if (propertyName.Equals (EffectShaderPropertyStr.UVScrollX)) {
			return "Main Texture UV Scroll X";
		} else if (propertyName.Equals (EffectShaderPropertyStr.UVScrollY)) {
			return "Main Texture UV Scroll Y";
		} else if (propertyName.Equals (EffectShaderPropertyStr.UVCutScrollX)) {
			return "CutOut UV Scroll X";
		} else if (propertyName.Equals (EffectShaderPropertyStr.UVCutScrollY)) {
			return "CutOut UV Scroll Y";
		} else if (propertyName.Equals (EffectShaderPropertyStr.UVMirrorX)) {
			return "X Mirror";
		} else if (propertyName.Equals (EffectShaderPropertyStr.UVMirrorY)) {
			return "Y Mirror";
		} else if (propertyName.Equals (EffectShaderPropertyStr.CutParticleSoftValue)) {
			return "Soft Particles Factor";
		} else if (propertyName.Equals (EffectShaderPropertyStr.DissolveSrc)) {
			return "Dissov Texture";
		} else if (propertyName.Equals (EffectShaderPropertyStr.SpecColor)) {
			return "Light Color";
		} else if (propertyName.Equals (EffectShaderPropertyStr.Shininess)) {
			return "Sjomomess";
		} else if (propertyName.Equals (EffectShaderPropertyStr.Amount)) {
			return "Amount";
		} else if (propertyName.Equals (EffectShaderPropertyStr.StartAmount)) {
			return "StartAmount";
		} else if (propertyName.Equals (EffectShaderPropertyStr.DissColor)) {
			return "DissColor";
		} else if (propertyName.Equals (EffectShaderPropertyStr.Illuminate)) {
			return "Illuminate";
		} else if (propertyName.Equals (EffectShaderPropertyStr.EmissionGain)) {
			return "EmissionGain";
		} else if (propertyName.Equals (EffectShaderPropertyStr.ShadowColor)) {
			return "ShadowColor";
		} else if (propertyName.Equals (EffectShaderPropertyStr.SpecularPower)) {
			return "SpecularPower";
		} else if (propertyName.Equals (EffectShaderPropertyStr.EdgeThickness)) {
			return "EdgeThickness";
		} else if (propertyName.Equals (EffectShaderPropertyStr.EdgeSaturtion)) {
			return "EdgeSaturtion";
		} else if (propertyName.Equals (EffectShaderPropertyStr.EdgeBrightness)) {
			return "EdgeBrightness";
		} else if (propertyName.Equals (EffectShaderPropertyStr.RimLightSampler)) {
			return "RimLightSampler";
		} else if (propertyName.Equals (EffectShaderPropertyStr.FalloffSampler)) {
			return "FalloffSampler";
		}
        return "";
    }
}
