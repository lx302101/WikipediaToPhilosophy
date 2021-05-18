

function displayGraphOnPage(treeData) {
    
    var value = 10;
    var color = "blue";
    var fillColor = "#add8e6";

    const margin = { top: 50, right: 90, bottom: 50, left: 90 },
        width = 800 - margin.left - margin.right,
        height = 800 - margin.top - margin.bottom;


    treemap = d3.tree().size([height, width]);
    let nodes = d3.hierarchy(treeData, d => d.children);
    nodes = treemap(nodes);

    svg = d3.select(".graph").append("svg")
        .attr("width", width + margin.left + margin.right)
        .attr("height", height + margin.top + margin.bottom), g = svg.append("g")
            .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

    node = g.selectAll(".node")
        .data(nodes.descendants())
        .enter().append("g")
        .attr("class", d => "node" + (d.children ? " node--internal" : " node--leaf"))
        .attr("transform", d => "translate(" + d.x + "," + d.y + ")");

    
    link = g.selectAll(".link") 
        .data(nodes.descendants().slice(1))
        .enter().append("path")
        .attr("class", "link")
        .style("stroke", d => d.data.level)
        .attr("d", d => {
            return "M" + d.x + "," + d.y + "C" + d.x + "," + (d.y + d.parent.y) / 2 + " " + d.parent.x + ","
                + (d.y + d.parent.y) / 2 + " " + d.parent.x + "," + d.parent.y;
        });
    
    node.append("a")
        .attr("xlink:href", function (d) { return "https://en.wikipedia.org/wiki/" + d.data.name })
        .append("circle")
        .attr("r", value)
        .style("stroke", color)
        .style("fill", fillColor);

    
    node.append("text") 
        .attr("dy", ".35em")
        .attr("y", function (d) { return d.children || d._children ? -18 : 18; })
        .style("text-anchor", 'middle')
        .style("fill-opacity", 1)
        .text(d => d.data.name);
}

function arrayToString(array) {
    var stringData3 = null;
    var tempString3 = `{ "name": "${array[array.length - 1]}", "children": null}`;
    for (let i = array.length - 2; i >= 0; i--) {
        stringData3 = `{ "name": "${array[i]}", "children": [${tempString3}] }`;
        tempString3 = stringData3;
    }
    return tempString3;
}

function mergeTrees(mainTree, tree2) {
    var temp1 = mainTree;
    var temp2 = tree2;
    var found = false;
    var j;
    while (found == false) {
        if ((temp1.children == null) && (temp2.children != null)) {
            temp1.children = ["temp"];
            temp1.children[0] = temp2.children[0];
            return mainTree;
        }
        if (temp2.children == null) {
            return mainTree;
        }
        for (let i = 0; i < temp1.children.length; ++i) {
            found = true;
            if (temp1.children[i].name == temp2.children[0].name) {
                j = i;
                found = false;
                break;
            }
        }
        if (found) {
            break;
        }
        temp1 = temp1.children[j];
        temp2 = temp2.children[0];
    }
    temp2 = temp2.children[0];
    temp1.children.push(temp2);
    return mainTree;
}
