﻿using System;
using Verse;

namespace ZombieLand
{
	public class VariableMaterial : IDisposable
	{
		DisposableMaterial material;
		MaterialRequest req;
		ColorData data;

		private bool disposed;

		public VariableMaterial(MaterialRequest req, ColorData data)
		{
			this.req = req;
			this.data = data;
		}

		public void Dispose()
		{
			Dispose(true);
		}

		private void Dispose(bool v)
		{
			if (!disposed)
			{
				material?.Dispose();
				material = null;
				disposed = true;
			}
		}

		public DisposableMaterial GetMaterial
		{
			get
			{
				if (material == null)
				{
					var mainTex = data.ToTexture();
					data = null;
					material = new DisposableMaterial(req.shader)
					{
						name = req.shader.name + "_" + mainTex.name,
						mainTexture = mainTex,
						color = req.color
					};
					if (req.maskTex != null)
					{
						material.SetTexture(ShaderPropertyIDs.MaskTex, req.maskTex);
						material.SetColor(ShaderPropertyIDs.ColorTwo, req.colorTwo);
					}
				}
				return material;
			}
		}
	}
}