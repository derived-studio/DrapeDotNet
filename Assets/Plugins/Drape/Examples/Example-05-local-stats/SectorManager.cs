using UnityEngine;
using UnityEngine.UI;
using Drape;
using Drape.Slug;

namespace Drape.Eamples.LocalStats
{
	public class SectorManager : MonoBehaviour
	{

		[SerializeField] private Transform _sectorUIContainer;
		[SerializeField] private Text _sectorProductionLabel;
		[SerializeField] private Text _rawProductionLabel;
		[SerializeField] private Button _improveSectorProductionBtn;
		[SerializeField] private Button _improveRawProductionBtn;

		private Registry Registry { get; set; }
		private Sector[] Sectors { get; set; }

		internal const string STAT_RAW_PRODUCTION = "Raw production";
		internal const string STAT_SECTOR_PROUCTION = "Sector production";
		internal const string MOD_IMPROVED_SECTOR_PRODUCTION = "Improved sector produciton";
		internal const string MOD_IMPROVED_RAW_PRODUCTION = "Improved raw produciton";
		internal const string MOD_ADDITIONAL_RAW_PRODUCTON = "Additional raw production";

		public SectorManager()
		{
			Registry = new Registry();
		}

		void Start()
		{
			RegisterStats();
			RegisterModifiers();
			InitializeComponents();

			RegisterButtonHandlers();		
			UpdateComponents();
		}

		private void Update()
		{
			UpdateComponents();
		}

		void UpdateComponents()
		{			
			float sumSector = 0;
			float sumTotal = 0;
			System.Array.ForEach(Sectors, sector => {
				sumTotal += sector.SectorProduction;
				sumSector += sector.RawProduction;				
			});

			_sectorProductionLabel.text = System.String.Format("Sector total: {0}", sumTotal);
			_rawProductionLabel.text = System.String.Format("Raw total: {0}", sumSector);

			for (var i = 0; i < 3; i ++) {
				Sector sector = _sectorUIContainer.GetChild(i).GetComponent<Sector>();
				sector.UpdateLabels();
			}
		}

		void RegisterStats()
		{
			Stat rawProduction = new Stat(new StatData() {
				Name = STAT_RAW_PRODUCTION,
				Value = 1
			}, Registry);

			Stat sectorProduction = new Stat(new StatData() {
				Name = STAT_SECTOR_PROUCTION
			}, Registry);
		}

		void RegisterModifiers()
		{
			Modifier additionalRawProduciotn = new Modifier(new ModifierData() {
				Name = MOD_ADDITIONAL_RAW_PRODUCTON,
				Stat = Code(STAT_RAW_PRODUCTION),
				RawFlat = 1
			}, Registry);

			Modifier improvedRawProduciotn = new Modifier(new ModifierData() {
				Name = MOD_IMPROVED_RAW_PRODUCTION,
				Stat = Code(STAT_RAW_PRODUCTION),
				FinalFactor = .5f
			}, Registry);

			Modifier improvedSectorProduction = new Modifier(new ModifierData() {
				Name = MOD_IMPROVED_SECTOR_PRODUCTION,
				Stat = Code(STAT_SECTOR_PROUCTION),
				FinalFactor = .5f
			}, Registry);
		}

		void InitializeComponents()
		{
			Stat rawProduction = Registry.Get<Stat>(Code(STAT_RAW_PRODUCTION));
			Stat sectorProduction = Registry.Get<Stat>(Code(STAT_SECTOR_PROUCTION));

			Sectors = new Sector[3];
			for (int i = 0; i < Sectors.Length; i++)
			{
				LocalStat localRawProduction = new LocalStat(rawProduction);
				LocalStat localSectorProd = new LocalStat(sectorProduction);

				Sectors[i] = _sectorUIContainer.GetChild(i).GetComponent<Sector>();
				Sectors[i].Init(i, localRawProduction, localSectorProd);
			}
		}

		void RegisterButtonHandlers()
		{
			_improveRawProductionBtn.onClick.AddListener(delegate
			{
				Modifier mod = Registry.Get<Modifier>(Code(MOD_IMPROVED_RAW_PRODUCTION));
				Registry.Get<Stat>(Code(STAT_RAW_PRODUCTION)).AddModifier(mod);
			});

			_improveSectorProductionBtn.onClick.AddListener(delegate
			{
				Modifier mod = Registry.Get<Modifier>(Code(MOD_IMPROVED_SECTOR_PRODUCTION));
				Registry.Get<Stat>(Code(STAT_SECTOR_PROUCTION)).AddModifier(mod);
			});
		}

		private string Code(string name)
		{
			return name.ToSlug();
		}

	}
}