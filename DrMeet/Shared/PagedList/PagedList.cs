﻿namespace DrMeet.Api.Shared.PagedList
{
    public class PagedList<T>
    {
        public List<T> List { get; set; } = [];
        public PagedListInfo Pagination { get; set; } = new();
    }
}
