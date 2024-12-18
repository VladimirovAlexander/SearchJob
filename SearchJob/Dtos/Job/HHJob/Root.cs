﻿namespace SearchJob.Dtos.Job.HHJob
{
    public class Root
    {
        public List<Item> items { get; set; }
        public int found { get; set; }
        public int pages { get; set; }
        public int page { get; set; }
        public int per_page { get; set; }
        public object clusters { get; set; }
        public object arguments { get; set; }
        public object fixes { get; set; }
        public object suggests { get; set; }
        public string alternate_url { get; set; }
    }
}
