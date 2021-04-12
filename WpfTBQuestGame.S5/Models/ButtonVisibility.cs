using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBQuestGame.PresentationLayer;

namespace TBQuestGame.Models
{
    public class ButtonVisibility : ObservableObject
    {

        #region FIELDS
        private bool _isVillageVisible;
        private bool _isMountainvisible;
        private bool _isForestVisible;
        private bool _isSwampVisible;
        private bool _isHarborVisible;
        private bool _isElfHoldVisible;
        private bool _isCaveVisible;
        private bool _isWitchesCampVisible;
        private bool _isIslandOfLostSoulsVisible;
        private bool _isAbyssVisible;
        private bool _isHomeVisible;
        #endregion

        #region PROPERTIES
        public bool IsVillageVisible
        {
            get { return _isVillageVisible; }
            set
            {
                _isVillageVisible = value;
                OnPropertyChanged(nameof(IsVillageVisible));
            }

        }
        public bool IsMountainVisible
        {
            get { return _isMountainvisible; }
            set
            {
                _isMountainvisible = value;
                OnPropertyChanged(nameof(IsMountainVisible));
            }
        }
        public bool IsForestVisible
        {
            get { return _isForestVisible; }
            set
            {
                _isForestVisible = value;
                OnPropertyChanged(nameof(IsForestVisible));
            }
        }
        public bool IsSwampVisible
        {
            get { return _isSwampVisible; }
            set
            {
                _isSwampVisible = value;
                OnPropertyChanged(nameof(IsSwampVisible));
            }
        }
        public bool IsHarborVisible
        {
            get { return _isHarborVisible; }
            set
            {
                _isHarborVisible = value;
                OnPropertyChanged(nameof(IsHarborVisible));
            }
        }
        public bool IsElfHoldVisible
        {
            get { return _isElfHoldVisible; }
            set
            {
                _isElfHoldVisible = value;
                OnPropertyChanged(nameof(IsElfHoldVisible));
            }
        }
        public bool IsCaveVisible
        {
            get { return _isCaveVisible; }
            set
            {
                _isCaveVisible = value;
                OnPropertyChanged(nameof(IsCaveVisible));
            }
        }
        public bool IsWitchesCampVisible
        {
            get { return _isWitchesCampVisible; }
            set
            {
                _isWitchesCampVisible = value;
                OnPropertyChanged(nameof(IsWitchesCampVisible));
            }
        }
        public bool IsIslandOfLostSoulsVisible
        {
            get { return _isIslandOfLostSoulsVisible; }
            set
            {
                _isIslandOfLostSoulsVisible = value;
                OnPropertyChanged(nameof(IsIslandOfLostSoulsVisible));
            }
        }
        public bool IsAbyssVisible
        {
            get { return _isAbyssVisible; }
            set
            {
                _isAbyssVisible = value;
                OnPropertyChanged(nameof(IsAbyssVisible));
            }
        }
        public bool IsHomeVisible
        {
            get { return _isHomeVisible; }
            set
            {
                _isHomeVisible = value;
                OnPropertyChanged(nameof(IsHomeVisible));
            }
        }
        #endregion

        public void ButtonUpdate(Location location)
        {
            switch (location.Id)
            {
                case 100:
                    IsVillageVisible = location.IsAccessible;
                    break;
                case 101:
                    IsMountainVisible = location.IsAccessible;
                    break;
                case 102:
                    IsForestVisible = location.IsAccessible;
                    break;
                case 103:
                    IsSwampVisible = location.IsAccessible;
                    break;
                case 104:
                    IsHarborVisible = location.IsAccessible;
                    break;
                case 105:
                    IsAbyssVisible = location.IsAccessible;
                    break;
                case 201:
                    IsCaveVisible = location.IsAccessible;
                    break;
                case 202:
                    IsElfHoldVisible = location.IsAccessible;
                    break;
                case 203:
                    IsWitchesCampVisible = location.IsAccessible;
                    break;
                case 204:
                    IsIslandOfLostSoulsVisible = location.IsAccessible;
                    break;
                case 300:
                    IsHomeVisible = location.IsAccessible;
                    break;

            }
        }
    }
}
