using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTable
{
   public  class course
    {
        private String name;
        private String time;
        private String place;
        private String teacher;
        private String points;
        private String type;
        public course(String name, String time, String place, String teacher, String points, String type)
        {
            this.name = name;
            this.time = time;
            this.place = place;
            this.teacher = teacher;
            this.points = points;
            this.type = type;
        }
        public void setTime(String time)
        {
            this.time = time;
        }
        public void setPlace(String place)
        {
            this.place = place;
        }
        public String getName()
        {
            return this.name;
        }
        public String getTime()
        {
            return this.time;
        }
        public String getPlace()
        {
            return this.place;
        }
        public String getTeacher()
        {
            return this.teacher;
        }
        public String getPoints()
        {
            return this.points;
        }
        public String getType()
        {
            return this.type;
        }
    }
}
