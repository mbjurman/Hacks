function ChannelController($scope) {
	$scope.channels = [];
	$scope.player = document.getElementById("audio-player");

    $scope.load = function() {
		var url = 'http://sverigesradio.se/api/v2/channels?pagination=false&format=json&callback=?';
		$.getJSON(url,function(data){
			$(data.channels).each(function(index,value) {
				var c = {
					name: value.name,
					imageurl: value.image,
					category: value.channeltype,
					url: value.liveaudio.url
				};
				if (c.imageurl !== '' && c.category !== "Lokal kanal" && c.category !== "Extrakanaler")
				{
					$scope.channels.push(c);
				}
			});
			$scope.$apply();
		});
    };

    $scope.play = function(channel) {
		$scope.player.src = channel.url;
    }

    $scope.load();
}

