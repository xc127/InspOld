using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicClass;

namespace Main
{
    partial class MainWindow
    {
        /// <summary>
        /// 显示语音
        /// </summary>
        void ShowVoice(int i)
        {
            try
            {                
                switch (i)
                {
                    case 1:
                        ShowVoice("平台1吹盘");
                        break;

                    case 2:
                        ShowVoice("平台2吹盘");
                        break;

                    case 3:
                        ShowVoice("顶层泡棉");
                        break;

                    case 4:
                        ShowVoice("底层泡棉");
                        break;

                    case 5:
                        ShowVoice("平台1下料");
                        
                        break;

                    case 6:
                        ShowVoice("平台2下料");
                        break;

                    case 7:
                        ShowVoice("请清理皮带");
                        break;
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        void ShowVoice(string name)
        {
            try
            {
                this.Dispatcher.Invoke(new Action(()=>
                    {
                        //UCVoicePlay.U_I.NameVoice = name;
                        //new Task(UCVoicePlay.U_I.StartPlay_Task).Start();
                    }
                
                ));
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
