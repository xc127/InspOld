using BasicClass;
using DealConfigFile;

namespace Main
{
    public partial class MainWindow
    {
        void ManualStartCycle(int index)
        {
            if (DealComprehensiveResult1.D_I.BlCyclePhotoStop 
                && DealComprehensiveResult2.D_I.BlCyclePhotoStop
                && DealComprehensiveResult3.D_I.BlCyclePhotoStop
                &&DealComprehensiveResult4.D_I.BlCyclePhotoStop)
            {
                ShowState("可以执行一键开始");

                DealComprehensive_Camera1_event(TriggerSource_enum.Null, index);

                if (ParCameraWork.NumCamera >1)
                {
                    DealComprehensive_Camera2_event(TriggerSource_enum.Null, index);                    
                }

                if (ParCameraWork.NumCamera > 2)
                {
                    DealComprehensive_Camera3_event(TriggerSource_enum.Null, index);
                }

                if (ParCameraWork.NumCamera > 3)
                {
                    DealComprehensive_Camera4_event(TriggerSource_enum.Null, index);
                }
                return;
            }
            ShowState("一键开始启动失败！");

        }

        void ManualStop()
        {
            //DealComprehensiveResult1.D_I.BlCyclePhotoStop = true;
            //DealComprehensiveResult2.D_I.BlCyclePhotoStop = true;
            //DealComprehensiveResult3.D_I.BlCyclePhotoStop = true;
            //DealComprehensiveResult4.D_I.BlCyclePhotoStop = true;
        }
    }
}
