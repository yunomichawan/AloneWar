namespace AloneWar.Stage.FieldObject
{
    /// <summary>
    /// 
    /// </summary>
    public class MassStatus
    {
        /// <summary>
        /// 
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Area { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// 通過可否(直接参照しないこと)
        /// </summary>
        public bool IsClose
        {
            get
            {
                return this.isClose;
            }
            set
            {
                this.isClose = value;

                
            }
        }
        private bool isClose = false;
    }
}
