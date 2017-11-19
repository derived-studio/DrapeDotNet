using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace Drape.Eamples.LocalStats
{
	using Slug;

	public class Sector : MonoBehaviour
	{
		[SerializeField] Text _sectorLabel;
		[SerializeField] Text _sectorProductionLabel;
		[SerializeField] Button _sectorImproveBtn;
		[SerializeField] Text _rawProductionLabel;
		[SerializeField] Button _rawImproveBtn;
		[SerializeField] Button _rawAddBtn;

		LocalStat _sectorProduction;
		LocalStat _rawProduction;
		
		public float SectorProduction
		{
			get
			{
				return _sectorProduction.GetValue(RawProduction);
			}
		}

		public float RawProduction
		{
			get
			{
				return _rawProduction.Value;
			}
		}

		public void Init(int index, LocalStat rawProduciton, LocalStat sectorProduction)
		{
			_sectorLabel.text = "Sector " + index.ToString();
			_rawProduction = rawProduciton;
			_sectorProduction = sectorProduction;

			UpdateLabels();
		}

		private void Start()
		{
			_sectorImproveBtn.onClick.AddListener(ImproveSectorProduction);
			_rawImproveBtn.onClick.AddListener(ImproveRawProduction);
			_rawAddBtn.onClick.AddListener(AddRawProduction);
		}

		private void ImproveSectorProduction()
		{
			Modifier mod = _sectorProduction.Registry.Get<Modifier>(Code(SectorManager.MOD_IMPROVED_SECTOR_PRODUCTION));
			_sectorProduction.AddModifier(mod);
			UpdateLabels();
		}

		private void ImproveRawProduction()
		{
			Modifier mod = _rawProduction.Registry.Get<Modifier>(Code(SectorManager.MOD_IMPROVED_RAW_PRODUCTION));
			_rawProduction.AddModifier(mod);
			UpdateLabels();
		}

		private void AddRawProduction()
		{
			Modifier mod = _rawProduction.Registry.Get<Modifier>(Code(SectorManager.MOD_ADDITIONAL_RAW_PRODUCTON));
			_rawProduction.AddModifier(mod);
			UpdateLabels();
		}

		public void UpdateLabels()
		{
			_sectorProductionLabel.text = String.Format("Sector: {0} ({1})", _sectorProduction.BaseValue, SectorProduction);
			_rawProductionLabel.text = String.Format("Raw: {0} ({1})", _rawProduction.BaseValue, RawProduction);
		}

		private string Code(string name)
		{
			return name.ToSlug();
		}
	}

}