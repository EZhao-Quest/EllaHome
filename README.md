if you want to deploy a jenkins slave with windows service core 2016 in ACI, we must waitting a few mins before connect to jenkins, this is a know isssue in MicroSoft, The delay of networking environment(DNS issue) happens under environment of kubernetes with specific windows version. The networking will not be ready when windows container starts in k8s. Usually it works in seconds. But sometimes it might take mins.

 Currently the non-deterministic time is caused by Networking. They already reported the issue to Windows team. There is unfortunately no mitigation as of now, hopefully it will be improved in next version.
 
they don’t have documentation published regarding this. they advice the value or waitting time should be 5 minus. In addition, 5 mins might be a bit long here. The delay varies from seconds – 5mins.  


workaround: 5 mins should not be necessary. 1 min should be enough. Another option is waiting first in container, then validate network is ready before starting business logic.

nanoserve and linux no this issue.


command: docker run [image] -url jenkinsServerRootUrl scerect agent_name 
