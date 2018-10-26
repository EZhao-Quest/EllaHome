if you want to deploy a jenkins slave with windows service core 2016 in ACI, we must waitting a few mins before connect to jenkins, this is a know isssue in MicroSoft, The delay of networking environment(DNS issue) happens under environment of kubernetes with specific windows version. Currently they don’t have documentation published regarding this. they advice the value or waitting time should be 5 minus.   In addition, 5 mins might be a bit long here. The delay varies from seconds – 5mins.  


nanoserve and linux no this issue.
