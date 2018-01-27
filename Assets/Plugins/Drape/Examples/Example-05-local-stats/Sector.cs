using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace Drape.Eamples.LocalStats
{
	using Slug;

	public class Sector : MonoBehaviour
	{
#pragma warning disable CS0649 // Field 'Sector._sectorLabel' is never assigned to, and will always have its default value null
		[SerializeField] Text _sectorLabel;
#pragma warning restore CS0649 // Field 'Sector._sectorLabel' is never assigned to, and will always have its default value null
#pragma warning disable CS0649 // Field 'Sector._sectorProductionLabel' is never assigned to, and will always have its default value null
		[SerializeField] Text _sectorProductionLabel;
#pragma warning restore CS0649 // Field 'Sector._sectorProductionLabel' is never assigned to, and will always have its default value null
#pragma warning disable CS0649 // Field 'Sector._sectorAddBtn' is never assigned to, and will always have its default value null
		[SerializeField] Button _sectorAddBtn;
#pragma warning restore CS0649 // Field 'Sector._sectorAddBtn' is never assigned to, and will always have its default value null
#pragma warning disable CS0649 // Field 'Sector._sectorImproveBtn' is never assigned to, and will always have its default value null
		[SerializeField] Button _sectorImproveBtn;
#pragma warning restore CS0649 // Field 'Sector._sectorImproveBtn' is never assigned to, and will always have its default value null
#pragma warning disable CS0649 // Field 'Sector._rawProductionLabel' is never assigned to, and will always have its default value null
		[SerializeField] Text _rawProductionLabel;
#pragma warning restore CS0649 // Field 'Sector._rawProductionLabel' is never assigned to, and will always have its default value null
#pragma warning disable CS0649 // Field 'Sector._rawImproveBtn' is never assigned to, and will always have its default value null
		[SerializeField] Button _rawImproveBtn;
#pragma warning restore CS0649 // Field 'Sector._rawImproveBtn' is never assigned to, and will always have its default value null
#pragma warning disable CS0649 // Field 'Sector._rawAddBtn' is never assigned to, and will always have its default value null
		[SerializeField] Button _rawAddBtn;
#pragma warning restore CS0649 // Field 'Sector._rawAddBtn' is never assigned to, and will always have its default value null

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
			_sectorAddBtn.onClick.AddListener(AddSectorProduction);
			_sectorImproveBtn.onClick.AddListener(ImproveSectorProduction);
			_rawImproveBtn.onClick.AddListener(ImproveRawProduction);
			_rawAddBtn.onClick.AddListener(AddRawProduction);
		}

		private void AddSectorProduction()
		{
			Modifier mod = _rawProduction.Registry.Get<Modifier>(Code(SectorManager.MOD_ADDITIONAL_RAW_PRODUCTON));
			_rawProduction.AddModifier(mod);
			UpdateLabels();
		}

		private void ImproveSectorProduction()
		{
			Modifier mod = _sectorProduction.Registry.Get<Modifier>(Code(SectorManager.MOD_IMPROVED_SECTOR_PRODUCTION));
			_sectorProduction.AddModifier(mod);
			UpdateLabels();
		}


		private void AddRawProduction()
		{
			Modifier mod = _rawProduction.Registry.Get<Modifier>(Code(SectorManager.MOD_ADDITIONAL_RAW_PRODUCTON));
			_rawProduction.AddModifier(mod);
			UpdateLabels();
		}

		private void ImproveRawProduction()
		{
			Modifier mod = _rawProduction.Registry.Get<Modifier>(Code(SectorManager.MOD_IMPROVED_RAW_PRODUCTION));
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