﻿using System;
using cn.jpush.api.common;
using cn.jpush.api.push.mode;
using cn.jpush.api.common.resp;
using cn.jpush.api.schedule;

namespace cn.jpush.api.example
{
    public class ScheduleApiExample
    {
        //run the DeviceApiExample first,it will add mobile,tags,alias to the device:
        //首先运行DeviceApiExample，它会为设备添加手机号码，标签别名，再运行JPushApiExample,ScheduleApiExample，步骤如下：
        //1.设置cn.jpush.api.example为启动项
        //2.在cn.jpush.api.example项目，右键选择属性，然后选择应用程序，最后在启动对象下拉框中选择DeviceApiExample
        //3.按照2的步骤设置，运行JPushApiExample,ScheduleApiExample.

        public static String TITLE = "Test from C# v3 sdk";
        public static String ALERT = "Test from  C# v3 sdk - alert";
        public static String MSG_CONTENT = "Test from C# v3 sdk - msgContent";
        public static String REGISTRATION_ID = "0900e8d85ef";
        public static String TAG = "tag_api";

        public static String NAME = "Test";
        public static bool ENABLED = true;
        public static String TIME = "2016-04-25 14:05:00";
        public static String INVALID_TIME = "2016-03-2514:05:00";
        public static String PUT_TIME = "2016-05-25 14:05:00";
        public static String PUT_NAME = "put_new_name";
        //创建成功后，填入你的schedule_id
        public static String PUT_SCHEDULE_ID = "d5ba84b2-f55b-11e5-8496-0021f653c902";
        
        public static String START = "2016-03-31 12:30:00";
        public static String END = "2016-05-12 12:30:00";
        public static String TIME_PERIODICAL = "14:00:00";
        public static String INVALID_TIME_PERIODICAL = "4:00:00";
        public static String TIME_UNIT = "WEEK";
        public static int FREQUENCY = 1;
        public static String[] POINT =new String[]{ "WED", "FRI"};

        public static int PAGEID = 1;
        public static String schedule_id ;
        public static String schedule_id1;
        public static String app_key = "6be9204c30b9473e87bad4dc";
        public static String master_secret = "8aae478411e89f7682ed5af6";

        static void Main(string[] args) {
            //init a pushpayload
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.all();
            pushPayload.audience = Audience.all();
            pushPayload.notification = new Notification().setAlert(ALERT);

            ScheduleClient scheduleclient = new ScheduleClient(app_key, master_secret);

            //init a TriggerPayload
            TriggerPayload triggerConstructor = new TriggerPayload(START, END, TIME_PERIODICAL, TIME_UNIT, FREQUENCY, POINT);
            //init a SchedulePayload
            SchedulePayload schedulepayloadperiodical = new SchedulePayload(NAME, ENABLED,triggerConstructor,pushPayload);

            try
            {
                var result = scheduleclient.sendSchedule(schedulepayloadperiodical);
                //由于统计数据并非非是即时的,所以等待一小段时间再执行下面的获取结果方法
                System.Threading.Thread.Sleep(10000);
                Console.WriteLine(result);
                //保留这里获取的schedule_id，作为后面删除schedule的参数，如果不想删除这个可以删掉这一行，另外设置一个schedule_id
                schedule_id = result.schedule_id;

            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorCode);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }


            SchedulePayload schedulepayloadsingle = new SchedulePayload();
            TriggerPayload triggersingle = new TriggerPayload(TIME);

            //SchedulePayload schedulepayloadsingle = new SchedulePayload();
            schedulepayloadsingle.setPushPayload(pushPayload);
            schedulepayloadsingle.setTrigger(triggersingle);
            schedulepayloadsingle.setName(NAME);
            schedulepayloadsingle.setEnabled(ENABLED);

            try
            {
                var result = scheduleclient.sendSchedule(schedulepayloadsingle);
                //由于统计数据并非非是即时的,所以等待一小段时间再执行下面的获取结果方法
                System.Threading.Thread.Sleep(10000);
                Console.WriteLine(result);
                //保留这里获取的schedule_id，作为后面删除schedule的参数，如果不想删除这个可以删掉这一行，另外设置一个schedule_id
                schedule_id = result.schedule_id;

            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorCode);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }


            //get schedule
            try
            {
                var result = scheduleclient.getSchedule(PAGEID);
                //由于统计数据并非非是即时的,所以等待一小段时间再执行下面的获取结果方法
                System.Threading.Thread.Sleep(10000);
                Console.WriteLine(result.schedules[0].name);

                //if the test Schedule is too much,delete it
                /*
                for (int counter = 0; counter <= 40; counter++) {
                    scheduleclient.deleteSchedule(result.schedules[counter].schedule_id);
                }
                */
                Console.WriteLine(result.schedules);
                Console.WriteLine(result);
            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorCode);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }


            //get schedule by id 
            try
            {
                var result = scheduleclient.getScheduleById(PUT_SCHEDULE_ID);
                //由于统计数据并非非是即时的,所以等待一小段时间再执行下面的获取结果方法
                System.Threading.Thread.Sleep(10000);
                Console.WriteLine(result.name);
                Console.WriteLine(result);
            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorCode);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }

            //put the name

            SchedulePayload putschedulepayload = new SchedulePayload();
            
            putschedulepayload.setName(NAME);
            /*
            putschedulepayload.setPushPayload(null);
            putschedulepayload.setTrigger(null);
            */
            //the default enabled is true,if you want to change it,you have to set it to false
            try
            {
                var result = scheduleclient.putSchedule(putschedulepayload, schedule_id);
                //由于统计数据并非非是即时的,所以等待一小段时间再执行下面的获取结果方法
                System.Threading.Thread.Sleep(10000);
                Console.WriteLine(result);
            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorCode);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }


            //deleteSchedule
            try
            {
                //删除的是第一次创建的schedule_id，如果要保留第一次创建的，请重新传入schedule_id
                var result = scheduleclient.deleteSchedule(schedule_id);
                //由于统计数据并非非是即时的,所以等待一小段时间再执行下面的获取结果方法
                System.Threading.Thread.Sleep(10000);
                Console.WriteLine(result);
            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorCode);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }

            //
            SchedulePayload schedulepayloadSet = new SchedulePayload();
            TriggerPayload triggerSet = new TriggerPayload();
            triggerSet.setStart(START);
            triggerSet.setEnd(END);
            triggerSet.setTime(TIME_PERIODICAL);
            triggerSet.setTime_unit(TIME_UNIT);
            triggerSet.setFrequency(FREQUENCY);
            triggerSet.setPoint(POINT);

            schedulepayloadSet.setPushPayload(pushPayload);
            schedulepayloadSet.setTrigger(triggerSet);
            schedulepayloadSet.setName(NAME);
            schedulepayloadSet.setEnabled(ENABLED);

            try
            {
                var result = scheduleclient.sendSchedule(schedulepayloadSet);
                //由于统计数据并非非是即时的,所以等待一小段时间再执行下面的获取结果方法
                System.Threading.Thread.Sleep(10000);
                Console.WriteLine(result);
                //保留这里获取的schedule_id，作为后面删除schedule的参数，如果不想删除这个可以删掉这一行，另外设置一个schedule_id
                schedule_id1 = result.schedule_id;

            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorCode);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }


        }

    }
}
