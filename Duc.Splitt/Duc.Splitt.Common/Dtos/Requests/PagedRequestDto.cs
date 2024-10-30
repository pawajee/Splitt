﻿namespace Duc.Splitt.Common.Dtos.Requests
{
    public abstract class PagedRequestDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }

}
